namespace ClickerWeb.Infrastructure.Abstractions;

public interface ICurrentUserAccessor
{
    Guid GetCurrentUserId();
}