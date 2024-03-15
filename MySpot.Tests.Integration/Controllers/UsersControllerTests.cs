using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure.Security;
using MySpot.Infrastructure.Time;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Integration.Controllers;

public class UsersControllerTests(OptionsProvider optionsProvider) : ControllerTests(optionsProvider), IDisposable
{
    [Fact]
    public async Task post_users_should_return_created_201_status_code()
    {
        var command = new SignUp(Guid.Empty, "test-user@myspot.io", "test-user", "secret", "John Doe",
            Role.User());

        var response = await Client.PostAsJsonAsync("users", command);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();
    }

    [Fact]
    public async Task post_sign_in_should_return_ok_200_status_code__and_jwt()
    {
        var password = "secure";
        var user = await AddUserAsync(password);

        var command = new SignIn(user.Email,password);
        var response = await Client.PostAsJsonAsync("users/sign-in", command);
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();
        jwt.ShouldNotBeNull();
        jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task get_users_me_should_return_ok_200_status_code_and_user()
    {
        var user = await AddUserAsync("secret");
        Authorize(user.Id, user.Role);

        var userDto = await Client.GetFromJsonAsync<UserDto>("Users/me");
        userDto.ShouldNotBeNull();
        userDto.Id.ShouldBe(user.Id.Value);
    }

    private async Task<User> AddUserAsync(string password)
    {
        var clock = new Clock();
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        
        var user = new User(Guid.NewGuid(), "test-user2@myspot.io", "uset-user-2",passwordManager.Secure(password),"John Doe" ,Role.User(),clock.Current());
        
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.SaveChangesAsync();
        return user;
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
    }
    private readonly TestDatabase _testDatabase = new TestDatabase();
    
    public void Dispose()
    {
        _testDatabase.Dispose();
    }
}