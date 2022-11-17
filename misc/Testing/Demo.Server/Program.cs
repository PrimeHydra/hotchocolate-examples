using Demo.Server.Types;
using Demo.Server.Data;
using Demo.Server.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<QueryType>()
    .AddMutationType<MutationType>()
    .AddType<PuppyType>()
    .AddType<AddPuppyInputType>()
    .Services
    .AddSingleton<IIdGenerator, IncrementingIdGenerator>()
    .AddSingleton<IRepository<Puppy>>(
        _ => new InMemoryRepository<Puppy>(
            DefaultDoggos.GetPuppies()
        ));

var app = builder.Build();

app.MapGraphQL();

app.Run();