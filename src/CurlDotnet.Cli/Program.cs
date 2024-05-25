using CurlDotnet.Commands;

AnsiConsole.Markup("[underline red]CurlDotnet [/]!");

var app = new CommandApp();

app.Configure(config =>
{
    _ = config.CaseSensitivity(CaseSensitivity.None)
              .AddCommand<GetCommand>("get");
});

return await app.RunAsync(args);
