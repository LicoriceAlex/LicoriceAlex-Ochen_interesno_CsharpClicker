using ClickerWeb.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using ClickerWeb.Domain;

namespace ClickerWeb.Initializers;

public static class DbContextInitializer
{
    public static void AddAppDbContext(IServiceCollection services)
    {
        var pathToDbFile = GetPathToDbFile();
        services
            .AddDbContext<AppDbContext>(options => options
                .UseSqlite($"Data Source={pathToDbFile}"));

        string GetPathToDbFile()
        {
            var applicationFolder = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "ClickerWeb");

            if (!Directory.Exists(applicationFolder))
            {
                Directory.CreateDirectory(applicationFolder);
            }

            return Path.Combine(applicationFolder, "ClickerWeb.db");
        }
    }

    public static void InitializeDbContext(AppDbContext appDbContext)
    {
        const string Rebelion = "Ребелион";
        const string EbonyIvory = "Эбони и Айвори";
        const string Yamato = "Ямато";
        const string DevilTrigger = "Дэвил Триггер";
        const string Dante = "Данте";
        const string Vergil = "Вергилий";
        const string Lady = "Леди";
        
        appDbContext.Database.Migrate();
        
        var existingBoosts = appDbContext.Boosts
            .ToArray();
        
        AddBoostIfNotExist(Rebelion, price: 100, profit: 1);
        AddBoostIfNotExist(EbonyIvory, price: 1000, profit: 10);
        AddBoostIfNotExist(DevilTrigger, price: 5000, profit: 50);
        AddBoostIfNotExist(Yamato, price: 20000, profit: 200);
        AddBoostIfNotExist(Dante, price: 50000, profit: 1000, isAuto: true);
        AddBoostIfNotExist(Vergil, price: 500000, profit: 5000, isAuto: true);
        AddBoostIfNotExist(Lady, price: 1000000, profit: 50000, isAuto: true);
        
        AddRandomUsers();
        
        appDbContext.SaveChanges();
        
        void AddBoostIfNotExist(string name, long price, long profit, bool isAuto = false)
        {
            if (!existingBoosts.Any(eb => eb.Title == name))
            {
                var pathToImage = Path.Combine(".", "Resources", "BoostImages", $"{name}.png");
                using var fileStream = File.OpenRead(pathToImage);
                using var memoryStream = new MemoryStream();
        
                fileStream.CopyTo(memoryStream);
        
                appDbContext.Boosts.Add(new Boost
                {
                    Title = name,
                    Price = price,
                    Profit = profit,
                    IsAuto = isAuto,
                    Image = memoryStream.ToArray(),
                });
            }
        }
        void AddRandomUsers()
        {
            if (appDbContext.Users.Count() > 100)
            {
                return;
            }
            const int limit = 2000;
            const int asciLimit = 126;
            const int symbolsLimit = 15;
            const int symbolsStart = 5;
            var random = new Random();
            for (var i = 0; i < 200; i++)
            {
                var score = random.Next(limit);
                var symbolsCount = random.Next(symbolsStart, symbolsLimit);
                var userName = string.Empty;
                for (var j = 0; j < symbolsCount; j++)
                {
                    var character = random.Next(asciLimit);
                    userName += char.ConvertFromUtf32(character);
                }
                appDbContext.Users.Add(new ApplicationUser
                {
                    UserName = userName,
                    RecordScore = score,
                });
            }
        }
    }
}