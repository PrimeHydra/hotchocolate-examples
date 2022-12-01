using Demo.Server.Types;
using Demo.Server.Data;
using Demo.Server.Models;
using Demo.Server.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLWithPuppies();

var app = builder.Build();

app.MapGraphQL();

app.Run();