using ClickerWeb.UseCases.GetBoosts;
using ClickerWeb.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClickerWeb.Controllers;

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

        var viewModel = new IndexViewModel()
        {
            Boosts = boosts,
        };

        return View(viewModel);
    }
}