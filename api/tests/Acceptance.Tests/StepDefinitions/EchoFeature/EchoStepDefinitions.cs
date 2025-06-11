using Acceptance.Tests.DataContracts;
using Acceptance.Tests.Utils;
using Shouldly;
using System.Net;

namespace Acceptance.Tests.StepDefinitions.EchoFeature;

[Binding, Scope(Feature = "echo")]
public class EchoStepDefinitions(HttpClient httpClient, ScenarioContext scenarioContext)
{
    const string contextResponseKey = "GetEchoResponseMessage";

    private HttpClient HttpClient { get; } = httpClient;
    private ScenarioContext ScenarioContext { get; } = scenarioContext;

    [When("a request is sent to {string} route")]
    public async Task WhenARequestIsSentToRoute(string route)
    {
        var response = await HttpClient.GetAsync(route)!;

        ScenarioContext.Add(contextResponseKey, response);
    }

    [Then("it should return success with status code {int}")]
    public void ThenItShouldReturnSuccessWithStatusCode(int statusCode)
    {
        var response = ScenarioContext.Get<HttpResponseMessage>(contextResponseKey);

        response.StatusCode.ShouldBe((HttpStatusCode)statusCode);
    }

    [Then("it should also return its name and version")]
    public async Task ThenItShouldAlsoReturnItsNameAndVersion(DataTable table)
    {
        var expectedResult = table.CreateInstance<EchoResponseMessage>();

        var response = ScenarioContext.Get<HttpResponseMessage>(contextResponseKey);

        var json = await response.Content.ReadAsStringAsync();

        var content = CustomJsonConverter.DeserializeObject<EchoResponseMessage>(json)!;

        content.ShouldNotBeNull();
        content.ShouldBeEquivalentTo(expectedResult);
    }
}
