using MediatR;

namespace ClickerWeb.UseCases.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<UserDto>;