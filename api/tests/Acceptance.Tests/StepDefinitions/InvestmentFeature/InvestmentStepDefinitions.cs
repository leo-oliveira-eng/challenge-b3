using Acceptance.Tests.DataContracts;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace Acceptance.Tests.StepDefinitions.InvestmentFeature;

[Binding, Scope(Feature = "Investment")]
public class InvestmentStepDefinitions(HttpClient httpClient, ScenarioContext scenarioContext)
{
    private const string requestMessagesKey = "InvestmentSimulationRequestMessage";
    private const string contextResponseKey = "InvestmentSimulationHttpResponses";
    private const string contentResponsesKey = "InvestmentSimulationContentResponses";

    private HttpClient HttpClient { get; } = httpClient;
    private ScenarioContext ScenarioContext { get; } = scenarioContext;

    [Given("the following request message")]
    public void GivenTheFollowingRequestMessage(DataTable table)
    {
        var requests = table.CreateSet<InvestmentSimulationRequestMessage>();

        ScenarioContext.Add(requestMessagesKey, requests);
    }

    [When("a POST is sent to {string} route")]
    public async Task WhenAPOSTIsSentToRoute(string route)
    {
        var requests = ScenarioContext.Get<IEnumerable<InvestmentSimulationRequestMessage>>(requestMessagesKey);

        var responses = new List<HttpResponseMessage>();

        foreach (var requestMessage in requests)
        {
            var response = await HttpClient.PostAsJsonAsync(route, requestMessage!);

            responses.Add(response!);
        }

        ScenarioContext.Add(contextResponseKey, responses);
    }

    [Then("it should return status code {int}")]
    public void ThenItShouldReturnStatusCode(int statusCode)
    {
        var responses = ScenarioContext.Get<List<HttpResponseMessage>>(contextResponseKey);

        responses.ForEach(response => response.StatusCode.ShouldBe((HttpStatusCode)statusCode));
    }

    [Then("it should return error message")]
    public void ThenItShouldReturnErrorMessage(DataTable table)
    {
        var expectedResult = table.CreateInstance<Message>();

        var responses = ScenarioContext.Get<List<HttpResponseMessage>>(contextResponseKey);

        responses.ForEach(async response =>
        {
            var content = await response.Content.ReadFromJsonAsync<Result<CdbSimulationResponseMessage>>();

            content!.Messages[0].ShouldBeEquivalentTo(expectedResult);
        });
    }

    [Then("it should return success")]
    public void ThenItShouldReturnSuccess()
    {
        var responses = ScenarioContext.Get<List<HttpResponseMessage>>(contextResponseKey);

        var contentResponses = new List<Result<CdbSimulationResponseMessage>>();

        responses.ForEach(async response =>
        {
            var content = await response.Content.ReadFromJsonAsync<Result<CdbSimulationResponseMessage>>();

            contentResponses.Add(content!);

            content!.IsSuccessful.ShouldBeTrue();
        });

        ScenarioContext.Add(contentResponsesKey, contentResponses);
    }

    [Then("it should have no messages")]
    public void ThenItShouldHaveNoMessages()
    {
        var contentResponses = ScenarioContext.Get<List<Result<CdbSimulationResponseMessage>>>(contentResponsesKey);

        foreach(var content in contentResponses)
            content.Messages.ShouldBeEmpty();
    }

    [Then("it should return following data")]
    public void ThenItShouldReturnFollowingData(DataTable table)
    {
        var expectedResult = table.CreateInstance<CdbSimulationResponseMessage>();

        var contentResponses = ScenarioContext.Get<List<Result<CdbSimulationResponseMessage>>>(contentResponsesKey);

        foreach(var content in contentResponses.Select(r => r.Data))
        {
            content!.Value.ShouldBeEquivalentTo(expectedResult);
        }
    }
}
