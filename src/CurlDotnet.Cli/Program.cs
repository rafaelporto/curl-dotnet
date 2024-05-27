using Microsoft.Extensions.DependencyInjection;
using CurlDotnet.Infrastructure;
using CurlDotnet.Commands;

AnsiConsole.Markup("[underline red]CurlDotnet [/]!");

var serviceCollection = new ServiceCollection();
serviceCollection.AddHttpClient();

var registrar = new TypeRegistrar(serviceCollection);

var app = new CommandApp(registrar);

app.Configure(config =>
{
    _ = config.CaseSensitivity(CaseSensitivity.None).AddCommand<GetCommand>("get");
});

return await app.RunAsync(args);