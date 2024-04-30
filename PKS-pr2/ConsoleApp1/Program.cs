using ConsoleApp1;
using ApplicationContext db = new ApplicationContext();

Console.WriteLine("Estates in %District where price is between %n and %m:");
foreach (var estate in Controller.EstatesInDistrictWithinPrice(1, 1000000, 2000000)) 
{
    Console.WriteLine(estate.Address);
}

Console.WriteLine("\n---\n");

Console.WriteLine("Agents who sold estates with 2 rooms:");
foreach (var agent in Controller.AgentsWhoSoldNRooms(2))
{
    Console.WriteLine($"{agent.LastName} {agent.FirstName} {agent.MiddleName}, {agent.Phone}");
}

Console.WriteLine("\n---\n");

Console.WriteLine("Summary cost of all 2-room estates in district:");
Console.WriteLine($"{Controller.CostOfAllNRoomEstatesInDistrict(2, 1)}");

Console.WriteLine("\n---\n");

Console.WriteLine("Minimum and maximum price of sells made by agent:");
var minmax = Controller.MinMaxSellByAgent(1);
Console.WriteLine($"Min: {minmax.Min()}; Max: {minmax.Max()}");

Console.WriteLine("\n---\n");

Console.WriteLine("Average safety review grade in sells by agent:");
Console.WriteLine($"{Controller.AverageSafetyRateByAgent(1, 3, 3)}");

Console.WriteLine("\n---\n");

Console.WriteLine("Amount of 2-floor estates in every district:");
Controller.EstatesAmountInDistrictsOnFloorN(2);

Console.WriteLine("\n---\n");

Console.WriteLine("Amount of apartments sold per agent:");
Controller.EstatesOfTypeSoldByAgents(1);

Console.WriteLine("\n---\n");

Console.WriteLine("Top 3 most expensive estates in every district:");
Controller.TopNEstatesByDistrict(3);

Console.WriteLine("\n---\n");

Console.WriteLine("Years when agent sold more than 2 estates:");
Controller.AgentYearsWhenSoldMoreThanNObjects(1, 2);

Console.WriteLine("\n---\n");

Console.WriteLine("Years when 2-3 new estates were introduced to the market:");
Controller.YearsWhereFromXtoYEstatesWereOpen(2, 3);

Console.WriteLine("\n---\n");

Console.WriteLine("Estates with difference between the declared price and the sell price being not more than 20%:");
Controller.EstatesWithDeclaredPriceDifferenceFromSoldPriceIsUpToNPercent(20);

Console.WriteLine("\n---\n");

Console.WriteLine("Estates which 1 square meter price is lower than average in their district:");
Controller.EstateAddressesWhereSquarePriceIsLessThanAverageIntTheirDistrict(1);

Console.WriteLine("\n---\n");

Console.WriteLine("Agents that haven't sold anything yet in 2024:");
Controller.AgentsThatShouldBeFired(2024);

Console.WriteLine("\n---\n");

Console.WriteLine("Sells in previous year and current year, + the change percentage per district:");
Controller.SellsAmountInYearandPreviousYearPerDistrict(2008);

Console.WriteLine("\n---\n");

Console.WriteLine("Reviews for a particular estate:");
Controller.elenaRevizor(1);