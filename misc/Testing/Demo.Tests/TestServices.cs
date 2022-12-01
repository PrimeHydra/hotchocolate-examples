using Demo.Server.Data;
using Demo.Server.Models;
using Demo.Server.Types;
using Demo.Server.Validators;

namespace Demo.Tests;

public static class TestServices
{
    static TestServices()
    {
        Services = new ServiceCollection()
            .AddGraphQLWithPuppies()
            .AddSingleton(
                sp => new RequestExecutorProxy(
                    sp.GetRequiredService<IRequestExecutorResolver>(),
                    Schema.DefaultName))
            .BuildServiceProvider();

        Executor = Services.GetRequiredService<RequestExecutorProxy>();
    }

    public static IServiceProvider Services { get; }

    public static RequestExecutorProxy Executor { get; }

    public static async Task<string> ExecuteRequestAsync(
        Action<IQueryRequestBuilder> configureRequest,
        IServiceScope serviceScope,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(serviceScope.ServiceProvider);
        configureRequest(requestBuilder);
        var request = requestBuilder.Create();

        // Note: can't do "await using" since we are on HC 12.x and I wanted to emulate that here
        using var result = await Executor.ExecuteAsync(request, cancellationToken);

        result.ExpectQueryResult();

        return result.ToJson();
    }
}