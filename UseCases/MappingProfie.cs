using AutoMapper;
using ClickerWeb.Domain;
using ClickerWeb.UseCases.GetBoosts;
using ClickerWeb.UseCases.GetCurrentUser;

namespace CSharpClicker.Web.UseCases;

public class MappingProfie : Profile
{
    public MappingProfie()
    {
        CreateMap<Boost, BoostDto>();
        CreateMap<UserBoost, UserBoostDto>();
        CreateMap<ApplicationUser, UserDto>();
    }
}