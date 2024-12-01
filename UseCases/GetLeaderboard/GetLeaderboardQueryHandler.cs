using AutoMapper;
using ClickerWeb.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClickerWeb.UseCases.GetLeaderboard;

public class GetLeaderboardQueryHandler : IRequestHandler<GetLeaderboardQuery, LeaderboardDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;
    
    public GetLeaderboardQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    public async Task<LeaderboardDto> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var usersByRecordScore = await mapper.ProjectTo<LeaderboardUserDto>(appDbContext
                .ApplicationUsers.OrderByDescending(user => user.RecordScore))
            .ToArrayAsync();

        return new LeaderboardDto()
        {
            Users = usersByRecordScore,
        };
    }
}