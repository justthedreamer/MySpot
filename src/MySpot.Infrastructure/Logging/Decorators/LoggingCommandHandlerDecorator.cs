using System.Diagnostics;
using Humanizer;
using Microsoft.Extensions.Logging;
using MySpot.Application.Abstractions;

namespace MySpot.Infrastructure.Logging.Decorators;

internal sealed class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly ILogger<ICommandHandler<TCommand>> _logger;

    public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler,
        ILogger<ICommandHandler<TCommand>> logger)
    {
        _commandHandler = commandHandler;
        _logger = logger;
    }
    public async Task HandleAsync(TCommand command)
    {
        var commandName = typeof(TCommand).Name.Underscore();
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        _logger.LogInformation("Started handling a command: {commandName}...",commandName);
        await _commandHandler.HandleAsync(command);
        stopwatch.Stop();
        _logger.LogInformation("Complete handling a command : {commandName} in {Elapsed}",commandName,stopwatch.Elapsed);
    }
}