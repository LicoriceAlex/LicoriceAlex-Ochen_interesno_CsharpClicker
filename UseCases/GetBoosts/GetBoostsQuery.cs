using MediatR;

namespace ClickerWeb.UseCases.GetBoosts;
public record GetBoostsQuery : IRequest<IReadOnlyCollection<BoostDto>>;