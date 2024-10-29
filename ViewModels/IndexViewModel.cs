using ClickerWeb.UseCases.GetBoosts;

namespace ClickerWeb.ViewModels;
public class IndexViewModel
{
    public IReadOnlyCollection<BoostDto> Boosts { get; init; }
}