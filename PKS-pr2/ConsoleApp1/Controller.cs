using System.Data.Common;
using System.Net;
using ConsoleApp1;
using ConsoleApp1.Models;

namespace ConsoleApp1;

public class Controller
{
    public static List<Estate> EstatesInDistrictWithinPrice(int districtId, double minPrice, double maxPrice)
    {
        //2.1
        using ApplicationContext db = new ApplicationContext();
        return db.Estates.Where(e => e.DistrictId == districtId && minPrice <= e.Cost && e.Cost <= maxPrice).ToList();
    }

    public static List<Agent> AgentsWhoSoldNRooms(int roomAmount)
    {
        //2.2
        using ApplicationContext db = new ApplicationContext();

        var confirmedAgentIds = (
            from sell in db.Sells
            join estate in db.Estates.Where(e => e.Rooms == roomAmount) on sell.EstateId equals estate.Id
            select new { AgentId = sell.AgentId }
        ).Distinct();

        var matchingAgents = (
            from agent in db.Agents
            join confirmedAgentId in confirmedAgentIds on agent.Id equals confirmedAgentId.AgentId
            select agent
        ).ToList();

        return matchingAgents;
        
        //This looks beautiful in my opinion, but since C# is a crappy language
        
        /*
        return (
            from agent in db.Agents
            join confirmedAgentId in
            db.Sells
                .Where(s => true)
                .GroupJoin(
                    db.Estates.Where(e => e.Rooms == roomAmount),
                    s => s.EstateId,
                    e => e.Id,
                    (sells, estates) => new
                        {
                            AgentId = sells.AgentId
                        })
            on agent.Id equals confirmedAgentId.AgentId select agent).ToList();
        */
    }
    
    public static double CostOfAllNRoomEstatesInDistrict(int roomAmount, int districtId)
    {
        //2.3
        using ApplicationContext db = new ApplicationContext();
        return db.Estates.Where(e => e.DistrictId == districtId && e.Rooms == roomAmount).Select(e => e.Cost).Sum();
    }

    public static List<double> MinMaxSellByAgent(int agentId)
    {
        //2.4
        using ApplicationContext db = new ApplicationContext();
        return new List<double>
        {
            db.Sells.Where(s => s.AgentId == agentId).Min(s => s.Cost),
            db.Sells.Where(s => s.AgentId == agentId).Max(s => s.Cost)
        };
    }

    public static double AverageSafetyRateByAgent(int agentId, int estateTypeId, int standardId)
    {
        //2.5
        using ApplicationContext db = new ApplicationContext();
        var sells = db.Sells.Where(s => s.AgentId == agentId);

        if (!sells.Any()) return 0;
        
        var estates = 
            from estate in db.Estates.Where(e => e.TypeId == estateTypeId) 
            join sell in sells on estate.Id equals sell.EstateId
            select new {Id = estate.Id};

        var reviews =
            from review in db.Reviews.Where(r => r.StandardId == standardId)
            join estate in estates on review.EstateId equals estate.Id
            select new { Grade = review.Grade };

        if (!reviews.Any()) return 0;
        
        return reviews.Average(r => r.Grade);
    }

    public static void EstatesAmountInDistrictsOnFloorN(int floorN)
    {
        //2.6
        using ApplicationContext db = new ApplicationContext();
        var estates = db.Estates.Where(e => e.Floor == floorN);
        var districts = db.Districts.Select(d => new
        {
            Id = d.Id,
            Title = d.Title,
            Amount = estates.Count(e => e.DistrictId == d.Id)
        });
        foreach (dynamic district in districts)
        {
            var title = district.Title;
            var amount = district.Amount;
            Console.WriteLine($"{title}: {amount}");
        }
    }

    public static void EstatesOfTypeSoldByAgents(int estateTypeId)
    {
        //2.7
        using ApplicationContext db = new ApplicationContext();
        var sells = db.Sells;
        var estates = from estate in db.Estates.Where(e => e.TypeId == estateTypeId)
            join sell in sells on estate.Id equals sell.EstateId
            select new { Id = estate.Id, AgentId = sell.AgentId };

        var agents = from agent in db.Agents
            join estate in estates on agent.Id equals estate.AgentId
            select new
            {
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                MiddleName = agent.MiddleName,
                Amount = estates.Count(e => e.AgentId == agent.Id)
            };

        foreach (var agent in agents.Distinct())
        {
            Console.WriteLine($"{agent.LastName} {agent.FirstName} {agent.MiddleName}: {agent.Amount}");
        }
    }

    public static void TopNEstatesByDistrict(int amount)
    {
        //2.8
        using ApplicationContext db = new ApplicationContext();

        var estates = db.Estates.Where(e => true).ToList();
        var topByDistrict = estates
            .GroupBy(e => e.DistrictId)
            .Select(group => new
            {
                DistrictId = group.Key,
                TopEstates = group.OrderByDescending(e => e.Cost).Take(amount)
            });

        var districts = db.Districts.Where(d => true).ToList();

        foreach (var district in topByDistrict)
        {
            var districtName = districts.First(d => d.Id == district.DistrictId).Title;
            Console.WriteLine($"Top {amount} estates in {districtName}:");
            foreach (var estate in district.TopEstates)
            {
                Console.WriteLine($"- {estate.Address}, {estate.Cost}, {estate.Floor}");
            }
        }
    }

    public static void AgentYearsWhenSoldMoreThanNObjects(int agentId, int n)
    {
        //2.9
        ApplicationContext db = new ApplicationContext();
        var sellYears = db.Sells
            .Where(s => s.AgentId == agentId).ToList()
            .Select(s => new { Year = Vrem.UnixTimeStampToYear(s.Date) });

        var properYears = sellYears.Distinct().Where(s => sellYears.Count(y => s.Year == y.Year) >= n);

        foreach (var year in properYears)
        {
            Console.WriteLine(year.Year);
        }
    }
    
    public static void YearsWhereFromXtoYEstatesWereOpen(int x, int y)
    {
        //2.10
        ApplicationContext db = new ApplicationContext();
        var estateYears = db.Estates
            .ToList()
            .Select(s => new { Year = Vrem.UnixTimeStampToYear(s.Date) });

        var properYears = estateYears
            .Distinct()
            .Where(s => Enumerable.Range(x, y).Contains(estateYears.Count(e => s.Year == e.Year)));

        foreach (var year in properYears)
        {
            Console.WriteLine(year.Year);
        }
    }

    public static void EstatesWithDeclaredPriceDifferenceFromSoldPriceIsUpToNPercent(int percent)
    {
        //2.11
        ApplicationContext db = new ApplicationContext();
        var fractionMax = 0.01 * (double)percent;

        var soldEstates = (from estate in db.Estates
            join sell in db.Sells on estate.Id equals sell.EstateId
            select new { 
                Address=estate.Address, 
                District=db.Districts.First(d => d.Id == estate.DistrictId).Title,
                Difference=estate.Cost == sell.Cost ? 0 : Math.Abs(sell.Cost - estate.Cost)*2/(sell.Cost+estate.Cost),
                Declared=estate.Cost,
                Sold=sell.Cost
            })
            .Where(e => e.Difference <= fractionMax);

        foreach (var estate in soldEstates)
        {
            Console.WriteLine($"{estate.Address}, {estate.District} ({estate.Difference*100}%: {estate.Declared} - {estate.Sold})");
        }
    }

    public static void EstateAddressesWhereSquarePriceIsLessThanAverageIntTheirDistrict(int estateTypeId)
    {
        //2.12
        ApplicationContext db = new ApplicationContext();

        var districts = db.Districts.ToList();

        var groups = db.Estates
            //.Where(e => e.TypeId == estateTypeId)
            .ToList()
            .Select(e => new
            {
                Address = e.Address,
                SqmPrice = e.Cost / e.Square,
                District = districts.First(d => d.Id == e.DistrictId)
            })
            .GroupBy(e => e.District.Id)
            .Select(group => new
            {
                DistrictId = group.Key,
                DistrictTitle = districts.First(d => d.Id == group.Key).Title,
                AveragePrice = group.Average(e => e.SqmPrice),
                Estates = group.Where(e => e.SqmPrice < group.Average(es => es.SqmPrice))
            });

        foreach (var group in groups)
        {
            Console.WriteLine($"{group.DistrictTitle} (average is {group.AveragePrice}):");
            foreach (var estate in group.Estates)
            {
                Console.WriteLine($"- {estate.Address} ({estate.SqmPrice})");
            }
        }
    }

    public static void AgentsThatShouldBeFired(int year)
    {
        //2.13
        ApplicationContext db = new ApplicationContext();
        var goodAgents =
            (from agent in db.Agents.ToList()
            join sell in db.Sells on agent.Id equals sell.AgentId
            select new
            {
                AgentId=agent.Id,
                Year=Vrem.UnixTimeStampToYear(sell.Date)
            })
            .Where(a => a.Year == year)
            .ToList();
        var badAgents = db.Agents
            .ToList()
            .Where(a => goodAgents.FirstOrDefault(g => g.AgentId == a.Id, null) == null)
            .ToList();
        

        foreach (var b in badAgents)
        {
            Console.WriteLine($"{b.LastName} {b.LastName} {b.MiddleName}");
        }
    }

    public static void SellsAmountInYearandPreviousYearPerDistrict(int year)
    {
        //2.14
        ApplicationContext db = new ApplicationContext();
        var sells = (from sell in db.Sells
            join estate in db.Estates on sell.EstateId equals estate.Id
            select new
            {
                Id = sell.Id,
                Year = Vrem.UnixTimeStampToYear(sell.Date),
                DistrictId = estate.DistrictId
            })
            .ToList();
        var districts = sells
            .GroupBy(s => s.DistrictId)
            .Select(group => new
            {
                DistrictId = group.Key,
                DistrictTitle = db.Districts.FirstOrDefault(d => d.Id == group.Key)?.Title,
                SellsThisYear = group.Count(s => s.Year == year),
                SellsLastYear = group.Count(s => s.Year == year - 1)
            })
            .Select(group => new
            {
                DistrictId = group.DistrictId,
                DistrictTitle = group.DistrictTitle,
                SellsThisYear = group.SellsThisYear,
                SellsLastYear = group.SellsLastYear,
                Change = (group.SellsLastYear != group.SellsThisYear ?
                    (group.SellsThisYear - group.SellsLastYear) /
                    (double)(group.SellsThisYear + group.SellsLastYear) *
                    2 : 0)
            });
        
        foreach (var district in districts)
        {
            Console.WriteLine($"{district.DistrictTitle}:");
            Console.WriteLine($" - {year - 1}: {district.SellsLastYear}");
            Console.WriteLine($" - {year}: {district.SellsThisYear}");
            Console.WriteLine($"   Change: {Math.Round(district.Change * 100, 2)}%");
        }
    }

    public static void elenaRevizor(int estateId)
    {
        //2.15
        ApplicationContext db = new ApplicationContext();

        var standards = db.Standards.ToList();

        var reviews = db.Reviews
            .Where(r => r.EstateId == estateId)
            .ToList()
            .GroupBy(r => r.StandardId)
            .Select(group => new
            {
                Standard = db.Standards.First(s => s.Id == group.Key).Title,
                Percent = group.Where(g => g.StandardId == group.Key).Average(g => g.Grade)
            })
            .Select(group => new
            {
                Standard = group.Standard,
                Percent = group.Percent,
                Grade = Grader.Grade((int)group.Percent)
            });
        
        foreach (var review in reviews)
        {
            Console.Write($"{review.Standard}: {review.Grade} из 5 | ");
            switch (review.Grade)
            {
                case 5: Console.WriteLine("Превосходно"); break;
                case 4: Console.WriteLine("Очень хорошо"); break;
                case 3: Console.WriteLine("Хорошо"); break;
                case 2: Console.WriteLine("Удовлетворительно"); break;
                case 1: Console.WriteLine("Неудовлетворительно"); break;
            }
        }
    }
}