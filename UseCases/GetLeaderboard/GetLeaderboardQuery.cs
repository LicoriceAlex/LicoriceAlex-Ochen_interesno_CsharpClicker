using MediatR;

namespace ClickerWeb.UseCases.GetLeaderboard;

public class GetLeaderboardQuery : IRequest<LeaderboardDto>;