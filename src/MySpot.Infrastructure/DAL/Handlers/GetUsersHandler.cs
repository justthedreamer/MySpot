using Microsoft.EntityFrameworkCore;
using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Infrastructure.DAL.Handlers;

internal class GetUsersHandler(MySpotDbContext dbContext) : IQueryHandler<GetUsers,IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
    {
        var users = await dbContext.Users
            .AsNoTracking()
            .ToListAsync();
        
        return users.Select(x => x.GetDto());
    }
}