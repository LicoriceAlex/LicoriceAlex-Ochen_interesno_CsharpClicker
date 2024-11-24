using ClickerWeb.UseCases.Common;
using MediatR;

namespace ClickerWeb.UseCases.AddPoints;

public record AddPointsCommand(int Clicks, int Seconds) : IRequest<ScoreDto>;