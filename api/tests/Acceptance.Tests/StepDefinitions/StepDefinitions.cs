using Acceptance.Tests.DataContracts;
using Acceptance.Tests.Utils;
using Shouldly;
using System.Net;

namespace Acceptance.Tests.StepDefinitions;

[Binding]
public class StepDefinitions(HttpClient httpClient)
{
    [Given("Cdb.Yield.Simulator.API is available")]
    public async Task GivenCdb_Yield_Simulator_APIIsAvailable()
    {
        HttpResponseMessage? response = await httpClient.GetAsync("api/echo");

        var json = await response.Content.ReadAsStringAsync();

        var contentResponse = CustomJsonConverter.DeserializeObject<EchoResponseMessage>(json);

        response!.StatusCode.ShouldBe(HttpStatusCode.OK);
        contentResponse!.Version.ShouldBe("1.0.0");
        contentResponse!.Name.ShouldBe("CDB Yield Simulator");
    }
}
