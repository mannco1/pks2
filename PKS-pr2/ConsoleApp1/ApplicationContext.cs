using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using Type = ConsoleApp1.Models.Type;

namespace ConsoleApp1;
public class ApplicationContext : DbContext
{
    public DbSet<Models.Type> Types { get; set; }
    public DbSet<Models.District> Districts { get; set; }
    public DbSet<Models.Material> Materials { get; set; }
    public DbSet<Models.Estate> Estates { get; set; }
    public DbSet<Models.Standard> Standards { get; set; }
    public DbSet<Models.Review> Reviews { get; set; }
    public DbSet<Models.Agent> Agents { get; set; }
    public DbSet<Models.Sell> Sells { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Type>().HasData(
            new Type {Id = 1, Title = "Квартира"},
            new Type {Id = 2, Title = "Дом"},
            new Type {Id = 3, Title = "Апартаменты"},
            new Type {Id = 4, Title = "Монастырь"}
        );
        modelBuilder.Entity<Material>().HasData(
            new Material {Id = 1, Title = "Дерево"},
            new Material {Id = 2, Title = "Киприч"},
            new Material {Id = 3, Title = "Обломки уран-графитового реактора"},
            new Material {Id = 4, Title = "Снег"},
            new Material {Id = 5, Title = "Шлакоблокунь"}
        );
        modelBuilder.Entity<District>().HasData(
            new District {Id = 1, Title = "Северный"},
            new District {Id = 2, Title = "Южный"},
            new District {Id = 3, Title = "Западный"},
            new District {Id = 4, Title = "Восточный"},
            new District {Id = 5, Title = "Верхний"},
            new District {Id = 6, Title = "Нижний"}
        );
        modelBuilder.Entity<Standard>().HasData(
            new Standard {Id = 1, Title = "Ремонт"},
            new Standard {Id = 2, Title = "Мебель"},
            new Standard {Id = 3, Title = "Безопасность"},
            new Standard {Id = 4, Title = "Транспорт"},
            new Standard {Id = 5, Title = "Соседи"}
        );
        modelBuilder.Entity<Estate>().HasData(
            new Estate { Id =  1, Cost = 1200000, Date = Vrem.Unix(2020, 1, 1), Address = "644041, г. Омск, ул. Лесная, 12", DistrictId = 3, MaterialId = 1, TypeId = 1, Floor = 5, Rooms = 2, Square = 10000000, Status = true, Description = "Что-то на богатом"},
            new Estate { Id =  2, Cost = 1500000, Date = Vrem.Unix(2020, 2, 2), Address = "614330, г. Пермь, ул. Лесная, 3", DistrictId = 1, MaterialId = 2, TypeId = 2, Floor = 1, Rooms = 2, Square = 200, Status = true, Description = "Что-то на богатом"},
            new Estate { Id =  3, Cost = 1400000, Date = Vrem.Unix(2020, 3, 1), Address = "344024, г. Ростов-на-Дону, ул. Зеленая, 37", DistrictId = 1, MaterialId = 3, TypeId = 3, Floor = 3, Rooms = 3, Square = 300, Status = false, Description = "Что-то на богатом"},
            new Estate { Id =  4, Cost = 1294777, Date = Vrem.Unix(2020, 5, 6), Address = "630796, г. Новосибирск, ул. Лесная, 20", DistrictId = 3, MaterialId = 4, TypeId = 4, Floor = 1, Rooms = 2, Square = 4000000, Status = true, Description = "Что-то на богатом"},
            new Estate { Id =  5, Cost = 3000000, Date = Vrem.Unix(2020, 6, 5), Address = "190577, г. Санкт-Петербург, ул. Свободы, 41", DistrictId = 3, MaterialId = 5, TypeId = 1, Floor = 6, Rooms = 4, Square = 2000, Status = false, Description = "Что-то на богатом"},
            new Estate { Id =  6, Cost = 5000000, Date = Vrem.Unix(2020, 11, 4), Address = "614005, г. Пермь, ул. Больничная, 1", DistrictId = 3, MaterialId = 1, TypeId = 2, Floor = 1, Rooms = 5, Square = 3000, Status = false, Description = "Что-то на богатом"},
            new Estate { Id =  7, Cost = 5,       Date = Vrem.Unix(2021, 4, 7), Address = "344279, г. Ростов-на-Дону, ул. Интернациональная, 14", DistrictId = 1, MaterialId = 2, TypeId = 3, Floor = 12, Rooms = 3, Square = 1200, Status = true, Description = "Бегите отсюда"},
            new Estate { Id =  8, Cost = 9999999, Date = Vrem.Unix(2021, 4, 23), Address = "394172, г. Воронеж, ул. Береговая, 9", DistrictId = 2, MaterialId = 3, TypeId = 4, Floor = 1, Rooms = 2, Square = 50000000, Status = true, Description = "Что-то на очень богатом"},
            new Estate { Id =  9, Cost = 3281084, Date = Vrem.Unix(2021, 5, 4), Address = "443564, г. Самара, ул. Заречная, 26", DistrictId = 3, MaterialId = 4, TypeId = 1, Floor = 5, Rooms = 7, Square = 600, Status = true, Description = "Цену задал кот, который прошелся по клавиатуре"},
            new Estate { Id = 10, Cost = 2000000, Date = Vrem.Unix(2022, 5, 2), Address = "454579, г. Челябинск, ул. Зеленая, 22", DistrictId = 4, MaterialId = 5, TypeId = 2, Floor = 1, Rooms = 4, Square = 700, Status = false, Description = "Вид классный"},
            new Estate { Id = 11, Cost = 2500000, Date = Vrem.Unix(2022, 1, 30), Address = "660738, г. Красноярск, ул. Чкалова, 36, кв", DistrictId = 5, MaterialId = 1, TypeId = 3, Floor = 2, Rooms = 3, Square = 80000, Status = true, Description = "Вид не очень классный, но это не повод занижать цену"},
            new Estate { Id = 12, Cost = 500000, Date = Vrem.Unix(2023, 1, 20), Address = "614868, г. Пермь, ул. Клубная, 43", DistrictId = 6, MaterialId = 2, TypeId = 2, Floor = 1, Rooms = 2, Square = 400, Status = false, Description = "Что-то на бедном"},
            new Estate { Id = 13, Cost = 100000, Date = Vrem.Unix(2023, 2, 5), Address = "190040, г. Санкт-Петербург, ул. Чапаева, 27", DistrictId = 1, MaterialId = 3, TypeId = 2, Floor = 17, Rooms = 2, Square = 450, Status = true, Description = "Что-то на бедном"},
            new Estate { Id = 14, Cost = 600000, Date = Vrem.Unix(2023, 3, 11), Address = "125274, г. Москва, ул. Нагорная, 44", DistrictId = 2, MaterialId = 4, TypeId = 2, Floor = 1, Rooms = 2, Square = 200, Status = false, Description = "Что-то на бедном"},
            new Estate { Id = 15, Cost = 300000, Date = Vrem.Unix(2024, 5, 12), Address = "660932, г. Красноярск, ул. Красная", DistrictId = 3, MaterialId = 5, TypeId = 2, Floor = 16, Rooms = 2, Square = 500, Status = false, Description = "Что-то на бедном"}
        );
        modelBuilder.Entity<Review>().HasData(
            new Review {Id = 1, Date = Vrem.Unix(2020,1,1), EstateId = 1, StandardId = 1, Grade = 100},
            new Review {Id = 2, Date = Vrem.Unix(2020,2,1), EstateId = 1, StandardId = 2, Grade = 70},
            new Review {Id = 3, Date = Vrem.Unix(2020,3,1), EstateId = 1, StandardId = 2, Grade = 30},
            new Review {Id = 4, Date = Vrem.Unix(2020,4,1), EstateId = 1, StandardId = 4, Grade = 70},
            new Review {Id = 5, Date = Vrem.Unix(2020,5,1), EstateId = 1, StandardId = 5, Grade = 100},
            new Review {Id = 6, Date = Vrem.Unix(2020,6,1), EstateId = 1, StandardId = 5, Grade = 50}
        );
        modelBuilder.Entity<Agent>().HasData(
            new Agent {Id = 1, FirstName = "Андрей", LastName = "Андерсон", MiddleName = "Анатольевич", Phone = "111-111-11-11"},
            new Agent {Id = 2, FirstName = "Борис", LastName = "Большаков", MiddleName = "Богданович", Phone = "222-222-22-22"},
            new Agent {Id = 3, FirstName = "Владимир", LastName = "Вавилов", MiddleName = "Викторович", Phone = "333-333-33-33"},
            new Agent {Id = 4, FirstName = "Глеб", LastName = "Герасимов", MiddleName = "Галактионович", Phone = "444-444-44-44"},
            new Agent {Id = 5, FirstName = "Дамир", LastName = "Донской", MiddleName = "Дмитриевич", Phone = "555-555-55-55"}
        );
        modelBuilder.Entity<Sell>().HasData(
            new Sell {Id = 1, AgentId = 1, EstateId = 1, Cost = 1200000, Date = Vrem.Unix(2007,3,1)},
            new Sell {Id = 2, AgentId = 1, EstateId = 1, Cost = 888, Date = Vrem.Unix(2021,3,1)},
            
            new Sell {Id = 3, AgentId = 1, EstateId = 10, Cost = 1980000, Date = Vrem.Unix(2000,3,1)},
            new Sell {Id = 4, AgentId = 1, EstateId = 11, Cost = 460000, Date = Vrem.Unix(2000,3,1)},
            new Sell {Id = 5, AgentId = 1, EstateId = 12, Cost = 415000, Date = Vrem.Unix(2007,3,1)},
            new Sell {Id = 6, AgentId = 1, EstateId = 13, Cost = 1200000, Date = Vrem.Unix(2007,3,1)},
            new Sell {Id = 7, AgentId = 1, EstateId = 14, Cost = 1200000, Date = Vrem.Unix(2007,3,1)},
            new Sell {Id = 8, AgentId = 1, EstateId = 15, Cost = 1200000, Date = Vrem.Unix(2008,3,1)},
            new Sell {Id = 9, AgentId = 4, EstateId = 6, Cost = 1200000, Date = Vrem.Unix(2008,3,1)},
            new Sell {Id = 10, AgentId = 4, EstateId = 5, Cost = 1200000, Date = Vrem.Unix(2007,3,1)},
            new Sell {Id = 11, AgentId = 4, EstateId = 4, Cost = 1200000, Date = Vrem.Unix(2008,3,1)},
            new Sell {Id = 12, AgentId = 2, EstateId = 8, Cost = 1200000, Date = Vrem.Unix(2024,3,1)},
            new Sell {Id = 13, AgentId = 3, EstateId = 7, Cost = 1200000, Date = Vrem.Unix(2024,3,1)}
        );
    }
    
    public ApplicationContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=db_PKS; Username=postgres; Password=1234");
    }
}