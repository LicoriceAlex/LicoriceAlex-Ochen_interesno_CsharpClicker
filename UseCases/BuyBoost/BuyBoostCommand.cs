using ClickerWeb.UseCases.Common;
using MediatR;

namespace ClickerWeb.UseCases.BuyBoost;

public record BuyBoostCommand(int BoostId) : IRequest<ScoreBoostDto>;