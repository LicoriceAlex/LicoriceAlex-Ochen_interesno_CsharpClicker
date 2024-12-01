using ClickerWeb.UseCases.GetLeaderboard;
using ClickerWeb.UseCases.SetUserAvatar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClickerWeb.Controllers;

[Route("user")]
public class UserController : Controller
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("avatar")]
    public async Task<IActionResult> SetAvatar(SetUserAvatarCommand command)
    {
        await mediator.Send(command);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("leaderboard")]
    public async Task<IActionResult> Leaderboard()
    {
        var leaderboard = await mediator.Send(new GetLeaderboardQuery());

        return View(leaderboard);
    }
}