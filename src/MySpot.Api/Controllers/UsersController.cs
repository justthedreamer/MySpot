using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Application.Security;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(
    ICommandHandler<SignUp> signUpHandler,
    IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler,
    IQueryHandler<GetUser, UserDto> getUserHandler,
    ICommandHandler<SignIn> signInHandler,
    ITokenStorage tokenStorage) : ControllerBase
{
    
    [HttpGet]
    public async Task<IEnumerable<UserDto>> Get([FromBody] GetUsers command)
    {
        return await getUsersHandler.HandleAsync(command);
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

    [HttpPost("sign-in")]
    public async Task<ActionResult<JwtDto>> Post(SignIn command)
    {
        await signInHandler.HandleAsync(command);
        var jwt = tokenStorage.Get();
        return Ok(jwt);
    }
}