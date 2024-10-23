using ClickerWeb.Domain;
using ClickerWeb.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace ClickerWeb.Initializers;

public static class IdentityInitializer
{
    public static void AddIdentity(IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.Configure<IdentityOptions>(o => o.Password.RequireNonAlphanumeric = false);
    }
}