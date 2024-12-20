using AutoMapper;
using ClickerWeb.Domain;
using ClickerWeb.UseCases.GetBoosts;
using ClickerWeb.UseCases.GetCurrentUser;
using ClickerWeb.UseCases.GetLeaderboard;
using ClickerWeb.UseCases.GetUserSettings;

namespace ClickerWeb.UseCases;

public class MappingProfie : Profile
{
    public MappingProfie()
    {
        CreateMap<Boost, BoostDto>();
        CreateMap<UserBoost, UserBoostDto>();
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<ApplicationUser, LeaderboardUserDto>();
        CreateMap<ApplicationUser, UserSettingsDto>();
    }
}