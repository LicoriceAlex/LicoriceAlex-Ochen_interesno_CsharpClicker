using ClickerWeb.Infrastructure.Abstractions;
using System.Security.Claims;

namespace ClickerWeb.Infrastructure.Implementations;

public class CurrentUserAccessor : ICurrentUserAccessor
{
    private IHttpContextAccessor contextAccessor;

    public CurrentUserAccessor(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        if (contextAccessor.HttpContext == null)
        {
            throw new InvalidOperationException("Cannot get HTTP context.");
        }

        var IdValue = contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(IdValue, out var userId))
        {
            throw new InvalidOperationException("Cannot parse user ID.");
        }

        return userId;
    }
}
