using MySpot.Application.Abstractions;
using MySpot.Application.Security;
using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

internal sealed class SignUpHandler(IUserRepository repository,IPasswordManager passwordManager,IClock clock) : ICommandHandler<SignUp>
{
    public async Task HandleAsync(SignUp command)
    {
        var id = new UserId(command.userId);
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(command.Password);
        var fullName = new FullName(command.FullName);
        var role = new Role(command.Role);

        if ((await repository.GetByEmailAsync(email)) is not null) 
            throw new UserAlreadyExistsException("User already exists.");

        if((await repository.GetByUsernameAsync(username)) is not null)
            throw new UserAlreadyExistsException("User already exists.");
        
        var securedPassword = passwordManager.Secure(password);
        var user = new User(id, email, username, securedPassword, fullName, role, clock.Current());
        
        await repository.AddAsync(user);
    }
}