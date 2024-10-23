using MediatR;

namespace ClickerWeb.UseCases.Logout;

public record LogoutCommand : IRequest<Unit>;