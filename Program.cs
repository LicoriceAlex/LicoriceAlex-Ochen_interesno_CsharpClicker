using ClickerWeb.Initializers;
using ClickerWeb.Domain;
using ClickerWeb.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using ClickerWeb.Infrastructure.Abstractions;

namespace ClickerWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        DbContextInitializer.InitializeDbContext(appDbContext);

        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();
        app.MapDefaultControllerRoute();
        app.MapHealthChecks("health");
        
        app.Run();
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddSwaggerGen();
        
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));
        
        services.AddAuthentication();
        services.AddAuthorization();
        services.AddControllersWithViews();

        services.AddScoped<IAppDbContext, AppDbContext>();

        IdentityInitializer.AddIdentity(services);
        DbContextInitializer.AddAppDbContext(services);
    }
}