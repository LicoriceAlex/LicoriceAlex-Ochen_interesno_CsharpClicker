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
        AddBoostIfNotExist(EbonyIvory, price: 500, profit: 15);
        AddBoostIfNotExist(DevilTrigger, price: 2000, profit: 60);
        AddBoostIfNotExist(Yamato, price: 5000, profit: 100);
        AddBoostIfNotExist(Dante, price: 10000, profit: 400, isAuto: true);
        AddBoostIfNotExist(Vergil, price: 100000, profit: 5000, isAuto: true);
        AddBoostIfNotExist(Lady, price: 1000000, profit: 50000, isAuto: true);
        
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
    }
}