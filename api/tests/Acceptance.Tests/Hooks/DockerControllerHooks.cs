using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Builders;
using Microsoft.Extensions.Configuration;
using Reqnroll.BoDi;
using System.Net;

namespace Acceptance.Tests.Hooks;

[Binding]
public class DockerControllerHooks
{
    #region Properties

    static ICompositeService CompositeService { get; set; } = null!;
    IObjectContainer ObjectContainer { get; }

    #endregion

    #region Constructors

    public DockerControllerHooks(IObjectContainer objectContainer)
    {
        ObjectContainer = objectContainer;
    }

    #endregion

    #region Setup Methods

    [BeforeTestRun]
    public static void DockerComposeUp()
    {
        var config = LoadConfiguration();

        var dockerComposeFileNames = config.GetSection("DockerComposeFileNames").Get<string[]>();

        var dockerComposePaths = GetDockerComposeLocation(dockerComposeFileNames!);

        var apiUrl = config["Cdb.Yield.Simulator.Api:BaseAddress"]!;
        
        dockerComposePaths.ToList().ForEach(Console.WriteLine);

        CompositeService = new Builder()
            .UseContainer()
            .DeleteIfExists()
            .UseCompose()
            .FromFile(dockerComposePaths)
            .RemoveOrphans()
            .RemoveAllImages()
            .WaitForHttp(
                service: "api_container",
                url: $"{apiUrl}/api/echo",
                continuation: (response, _) =>
                {
                    var result = response.Code != HttpStatusCode.OK ? 10000 : 0;
                    Console.WriteLine($"Response Code received: {response.Code}");

                    return result;
                })
            .Build()
            .Start();
    }

    private static IConfiguration LoadConfiguration()
        => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

    private static string[] GetDockerComposeLocation(string[] dockerComposeFileNames)
    {
        var directory = Directory.GetCurrentDirectory();

        var dockerComposePaths = new List<string>();

        foreach (var file in dockerComposeFileNames)
        {
            dockerComposePaths.Add(GetDockerComposePath(directory, file));
        }

        return [.. dockerComposePaths];
    }

    private static string GetDockerComposePath(string directory, string file)
    {
        while (!Directory.EnumerateFiles(directory, "*.yml").Any(fileName => fileName.EndsWith(file)))
        {
            directory = directory[..directory.LastIndexOf(Path.DirectorySeparatorChar)];
        }

        return Path.Combine(directory, file);
    }

    [AfterTestRun]
    public static void DockerComposeDown()
    {
        CompositeService.Stop();
        CompositeService.Dispose();
    }

    [BeforeScenario]
    public void AddHttpClient()
    {
        var config = LoadConfiguration();

        var httpClient = new HttpClient { BaseAddress = new Uri(config["Cdb.Yield.Simulator.Api:BaseAddress"]!) };

        httpClient.DefaultRequestHeaders.Clear();

        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        ObjectContainer.RegisterInstanceAs(httpClient);
    }

    #endregion
}
