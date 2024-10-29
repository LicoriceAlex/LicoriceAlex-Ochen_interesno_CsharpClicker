using AutoMapper;
using ClickerWeb.Domain;
using ClickerWeb.UseCases.GetBoosts;

namespace ClickerWeb.UseCases;

public class MappingProfie : Profile
{
    public MappingProfie()
    {
        CreateMap<Boost, BoostDto>();
    }
}