using MyProject.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddGraphQLServer()
        .AddQueryType<HuffmanQuery>(x => x.Name(nameof(HuffmanQuery)))
        .SetRequestOptions(_ => new HotChocolate.Execution.Options.RequestExecutorOptions { ExecutionTimeout = TimeSpan.FromMinutes(15) })
        .BindRuntimeType<char, StringType>()
        .AddTypeConverter<char, string>(from => from.ToString());

var app = builder.Build();
app.MapGraphQL("/graphql");

app.Run();