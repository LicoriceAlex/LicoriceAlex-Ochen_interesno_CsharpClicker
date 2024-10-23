using MediatR;

namespace ClickerWeb.UseCases.Register;

public record RegisterCommand(string UserName, string Password) : IRequest<Unit>;