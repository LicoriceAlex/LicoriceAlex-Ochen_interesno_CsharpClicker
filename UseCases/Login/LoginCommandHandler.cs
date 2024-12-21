using ClickerWeb.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ClickerWeb.UseCases.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Unit>
{
    private SignInManager<ApplicationUser> signInManager;
    private UserManager<ApplicationUser> userManager;

    public LoginCommandHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public async Task<Unit> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ValidationException("Введите имя пользователя и пароль");
        }
        var user = await userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            throw new ValidationException("Пользователь не найден");
        }

        request.ToString();

        var result = await signInManager.PasswordSignInAsync(user, request.Password, isPersistent: true, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new ValidationException("Неверный пароль");
        }

        return Unit.Value;
    }
}