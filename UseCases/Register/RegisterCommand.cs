using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ClickerWeb.UseCases.Register;

//public record RegisterCommand(string UserName, string Password) : IRequest<Unit>;

public class RegisterCommand : IRequest<Unit>
{
    [Required(ErrorMessage = "Введите имя пользователя")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Введите пароль")]
    public string Password { get; set; }
}