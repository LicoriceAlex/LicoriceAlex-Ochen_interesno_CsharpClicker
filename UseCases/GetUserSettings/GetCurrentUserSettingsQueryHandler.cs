using AutoMapper;
using ClickerWeb.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClickerWeb.UseCases.GetUserSettings;

public class GetCurrentUserSettingsQueryHandler : IRequestHandler<GetCurrentUserSettingsQuery, UserSettingsDto>
{
    private readonly IMapper mapper;
    private readonly IAppDbContext appDbContext;
    private readonly ICurrentUserAccessor currentUserAccessor;

    public GetCurrentUserSettingsQueryHandler(IMapper mapper, IAppDbContext appDbContext, ICurrentUserAccessor currentUserAccessor)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
        this.currentUserAccessor = currentUserAccessor;
    }

    public async Task<UserSettingsDto> Handle(GetCurrentUserSettingsQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserAccessor.GetCurrentUserId();

        var user = await appDbContext.ApplicationUsers
            .Include(user => user.UserBoosts)
            .ThenInclude(ub => ub.Boost)
            .FirstAsync(user => user.Id == userId);

        return mapper.Map<UserSettingsDto>(user);
    }
}