using MediatR;

namespace ClickerWeb.UseCases.GetLeaderboard;

public record GetLeaderboardQuery(int Page = 1) : IRequest<LeaderboardDto>;