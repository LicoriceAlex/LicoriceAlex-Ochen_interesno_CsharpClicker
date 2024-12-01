using MediatR;

namespace ClickerWeb.UseCases.SetUserAvatar;

public record SetUserAvatarCommand(IFormFile Avatar) : IRequest<Unit>;