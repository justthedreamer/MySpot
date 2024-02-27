using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(
    ICommandHandler<SignUp> signUpHandler,
    IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler,
    IQueryHandler<GetUser, UserDto> getUserHandler) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<UserDto>> Get()
    {
        return await getUsersHandler.HandleAsync(new GetUsers());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        return await getUserHandler.HandleAsync(new GetUser { UserId = id });
    }

    [HttpPost]
    public async Task<ActionResult> Post(SignUp command)
    {
        var id = Guid.NewGuid();
        command = command with { userId = id };
        await signUpHandler.HandleAsync(command);
        return NoContent();
    }
}