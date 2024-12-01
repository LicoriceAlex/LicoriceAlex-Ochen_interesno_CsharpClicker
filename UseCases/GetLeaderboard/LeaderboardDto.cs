namespace ClickerWeb.UseCases.GetLeaderboard;

public class LeaderboardDto
{
    public IReadOnlyCollection<LeaderboardUserDto> Users { get; init; }
}