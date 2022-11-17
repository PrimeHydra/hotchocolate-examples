using Demo.Server.Data;
using Demo.Server.Models;
using Demo.Server.Types;

namespace Demo.Tests;

public static class TestServices
{
    static TestServices()
    {
        Services = new ServiceCollection()
            .AddGraphQLServer()
            .AddQueryType<QueryType>()
            .AddMutationType<MutationType>()
            .AddType<PuppyType>()
            .AddType<AddPuppyInputType>()
            .Services

            // Use scoped data services so we can give each test class instance its own scope
            .AddScoped<IIdGenerator, IncrementingIdGenerator>()
            .AddScoped<IRepository<Puppy>>(
                _ => new InMemoryRepository<Puppy>(
                    DefaultDoggos.GetPuppies()
                ))
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