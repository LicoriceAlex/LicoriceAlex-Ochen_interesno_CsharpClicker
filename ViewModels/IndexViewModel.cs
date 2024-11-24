using ClickerWeb.UseCases.GetBoosts;
using ClickerWeb.UseCases.GetCurrentUser;

namespace ClickerWeb.ViewModels;

public class IndexViewModel
{
    public UserDto User { get; init; }

    public IReadOnlyCollection<BoostDto> Boosts { get; init; }
}