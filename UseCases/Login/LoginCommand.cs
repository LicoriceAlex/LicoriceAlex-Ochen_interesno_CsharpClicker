using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ClickerWeb.UseCases.Login;

//public record LoginCommand(string UserName, string Password) : IRequest<Unit>;

public class LoginCommand : IRequest<Unit>
{
    [Required(ErrorMessage = "Введите имя пользователя")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Введите пароль")]
    public string Password { get; set; }
}