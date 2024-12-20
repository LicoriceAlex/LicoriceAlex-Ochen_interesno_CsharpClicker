using MediatR;
namespace ClickerWeb.UseCases.GetUserSettings;

public record GetCurrentUserSettingsQuery : IRequest<UserSettingsDto>;