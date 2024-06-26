using System.Net;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Integration.Controllers;

public class HomeControllerTests(OptionsProvider optionsProvider) : ControllerTests(optionsProvider)
{
    [Fact]
    public async Task get_base_endpoint_should_return_200_ok_status_code_and_api_name()
    {
        var response = await Client.GetAsync("/");
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldBe("MySpot API [test]");
        
    }
}