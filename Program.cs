using ClickerWeb.Infrastructure.Abstractions;
using ClickerWeb.Infrastructure.DataAccess;
using ClickerWeb.Infrastructure.Implementations;
using ClickerWeb.Initializers;

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

        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();
        app.MapDefaultControllerRoute();
        app.MapHealthChecks("health-check");
        
        app.MapGet("/Account/Login", context =>
        {
            context.Response.Redirect("/auth/login");
            return Task.CompletedTask;
        });

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddSwaggerGen();

        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));
        
        services.AddAuthorization();
        services.AddControllersWithViews();

        services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
        services.AddScoped<IAppDbContext, AppDbContext>();

        IdentityInitializer.AddIdentity(services);
        
        services.ConfigureApplicationCookie(options =>
            { options.LoginPath = "/auth/login"; });

        DbContextInitializer.AddAppDbContext(services);
    }
}