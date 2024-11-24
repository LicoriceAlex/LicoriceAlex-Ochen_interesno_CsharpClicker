using ClickerWeb.UseCases.AddPoints;
using ClickerWeb.UseCases.Common;
using ClickerWeb.UseCases.GetBoosts;
using ClickerWeb.UseCases.GetCurrentUser;
using ClickerWeb.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IMediator mediator;

    public HomeController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var boosts = await mediator.Send(new GetBoostsQuery());
        var user = await mediator.Send(new GetCurrentUserQuery());

        var viewModel = new IndexViewModel()
        {
            Boosts = boosts,
            User = user,
        };

        return View(viewModel);
    }

    [HttpPost("score")]
    public async Task<ScoreDto> AddToScore(AddPointsCommand command)
        => await mediator.Send(command);
}