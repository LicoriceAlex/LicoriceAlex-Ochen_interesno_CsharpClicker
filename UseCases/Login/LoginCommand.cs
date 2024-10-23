using MediatR;

namespace ClickerWeb.UseCases.Login;

public record LoginCommand(string UserName, string Password) : IRequest<Unit>;