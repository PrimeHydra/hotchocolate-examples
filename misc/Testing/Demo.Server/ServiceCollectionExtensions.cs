using Demo.Server.Data;
using Demo.Server.Models;
using Demo.Server.Types;
using Demo.Server.Validators;

namespace Demo.Server
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGraphQLWithPuppies(this IServiceCollection services)
        {
            return services.AddGraphQLServer()
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
                .AddScoped<GetPuppyInputValidator>();
        }
    }
}