using ClickerWeb.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ClickerWeb.UseCases.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly UserManager<ApplicationUser> userManager;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ValidationException("Введите имя пользователя и пароль");
        }
        if (userManager.Users.Any(u => u.UserName == request.UserName))
        {
            throw new ValidationException("Пользователь с таким именем уже существует");
        }

        var user = new ApplicationUser
        {
            UserName = request.UserName,
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errorDescriptions = result.Errors.Select(e => e.Description);
            var errorString = string.Join(Environment.NewLine, errorDescriptions);
            throw new ValidationException(errorString);
        }

        return Unit.Value;
    }
}