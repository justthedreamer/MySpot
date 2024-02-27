using System.Windows.Input;
using ICommand = MySpot.Application.Abstractions.ICommand;

namespace MySpot.Application.Commands;

public record SignIn(string Email,string Password) : ICommand
{
    
}