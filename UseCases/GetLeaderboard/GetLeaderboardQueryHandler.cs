using AutoMapper;
using ClickerWeb.Infrastructure.Abstractions;
using ClickerWeb.Domain;
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
        var offset = (request.Page - 1) * DomainConstants.PageSize;

        var usersByRecordScore = await mapper.ProjectTo<LeaderboardUserDto>(appDbContext
                .ApplicationUsers
                .OrderByDescending(user => user.RecordScore)
                .Skip(offset)
                .Take(DomainConstants.PageSize))
            .ToArrayAsync();

        var usersTotal = appDbContext.ApplicationUsers.Count();
        var pagesTotal = usersTotal % DomainConstants.PageSize == 0
            ? usersTotal / DomainConstants.PageSize
            : usersTotal / DomainConstants.PageSize + 1;

        return new LeaderboardDto()
        {
            Users = usersByRecordScore,
            PageInfo = new PageInfoDto
            {
                Page = request.Page,
                Total = pagesTotal,
            }
        };
    }
}