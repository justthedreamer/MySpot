using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Application.Security;
using Swashbuckle.AspNetCore.Annotations;

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
    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserDto>> Get()
    {
        if (string.IsNullOrWhiteSpace(HttpContext.User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(HttpContext.User.Identity.Name);
        var user = await getUserHandler.HandleAsync(new GetUser() { UserId = userId });
        if (user is null)
        {
            return NotFound();
        }

        return user;
    }
    
    [HttpGet]
    [Authorize(Policy = "is-admin")]
    [SwaggerOperation("Get single by user ID if exists")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromBody] GetUsers command)
    {
        if (!HttpContext.User.IsInRole("admin"))
        {
            return Forbid();
        }
        
        var users = await getUsersHandler.HandleAsync(command);

        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "is-admin")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        var user = await getUserHandler.HandleAsync(new GetUser { UserId = id });
        if (user is null)
        {
            return NotFound();
        }
        return user;
    }

    [HttpPost]
    public async Task<ActionResult> Post(SignUp command)
    {
        var id = Guid.NewGuid();
        command = command with { userId = id };
        await signUpHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new {command.userId},null);
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<JwtDto>> Post(SignIn command)
    {
        await signInHandler.HandleAsync(command);
        var jwt = tokenStorage.Get();
        return Ok(jwt);
    }
}