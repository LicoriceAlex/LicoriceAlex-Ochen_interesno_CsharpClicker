using MediatR;

namespace ClickerWeb.UseCases.GetUserSettings;

public record UserSettingsDto
{
    public string UserName { get; init; }

    public byte[] Avatar { get; init; }
}