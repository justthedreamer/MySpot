using System.Security.Authentication;
using MySpot.Application.Abstractions;
using MySpot.Application.Security;
using MySpot.Core.Repositories;

namespace MySpot.Application.Commands.Handlers;

internal sealed class SignInHandler(IUserRepository userRepository, IAuthenticator authenticator,  IPasswordManager passwordManager,ITokenStorage tokenStorage) : ICommandHandler<SignIn>
{
    public async Task HandleAsync(SignIn command)
    {
        var user = await userRepository.GetByEmailAsync(command.Email);
        if (user is null)
        {
            throw new InvalidCredentialException();
        }

        if (!passwordManager.Validate(command.Password, user.Password))
        {
            throw new InvalidCredentialException();
        }

        var jwt = authenticator.CreateToken(user.Id, user.Role);
        tokenStorage.Set(jwt);
    }
}