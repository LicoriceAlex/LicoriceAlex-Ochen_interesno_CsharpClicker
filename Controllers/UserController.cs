using ClickerWeb.UseCases.GetLeaderboard;
using ClickerWeb.UseCases.SetUserAvatar;
using ClickerWeb.UseCases.GetUserSettings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ClickerWeb.ViewModels;

namespace ClickerWeb.Controllers;

[Authorize]
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
        try
        {
            await mediator.Send(command);
            return RedirectToAction("Index", "Home");
        }
        catch (ValidationException ex)
        {
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpGet("leaderboard")]
    public async Task<IActionResult> Leaderboard(GetLeaderboardQuery query)
    {
        var leaderboard = await mediator.Send(query);

        return View(leaderboard);
    }

    [HttpGet("settings")]
     public async Task<IActionResult> Settings()
     {
         var userSettings = await mediator.Send(new GetCurrentUserSettingsQuery());
         return View(userSettings);
     }
}