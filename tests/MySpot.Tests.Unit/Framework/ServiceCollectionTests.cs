using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Unit.Framework;

public class ServiceCollectionTests
{
    [Fact]
    public void test()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IMessenger,Messenger>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        using (var scope = serviceProvider.CreateScope())
        {
            var messenger = scope.ServiceProvider.GetRequiredService<IMessenger>();
            messenger.Send();

            var messenger2 = scope.ServiceProvider.GetRequiredService<IMessenger>();
            messenger2.Send();;

            messenger.ShouldNotBeNull();
            messenger2.ShouldNotBeNull();
            messenger.ShouldBe(messenger2);
        }
        
        using (var scope = serviceProvider.CreateScope())
        {
            var messenger = scope.ServiceProvider.GetRequiredService<IMessenger>();
            messenger.Send();

            var messenger2 = scope.ServiceProvider.GetRequiredService<IMessenger>();
            messenger2.Send();;

            messenger.ShouldNotBeNull();
            messenger2.ShouldNotBeNull();
            messenger.ShouldBe(messenger2);
        }
        
    }

    private interface IMessenger
    {
        void Send();
    }

    private class Messenger : IMessenger
    {
        private readonly Guid _id = Guid.NewGuid();

        public void Send() => Console.WriteLine($"Sending a message... ${_id}");
    }
}