using System.ComponentModel;

namespace CurlDotnet.Commands;

internal sealed class GetCommand : AsyncCommand<GetSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, GetSettings settings)
    {
        //TODO: Abstrair mais essa parte
        AnsiConsole.MarkupLine($"[bold]Request Details:[/] {settings.Verbose} \n");

        var httpClient = new HttpClient();

        var message = new HttpRequestMessage(HttpMethod.Get, settings.Uri);

        PrintRequest(message, settings.Verbose);

        HttpResponseMessage response = await httpClient.SendAsync(message);

        if (response.IsSuccessStatusCode)
        {
            await PrintResult(response, settings.Verbose);
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        Console.WriteLine("Request Details:i " + settings.Verbose);

        return 0;
    }

    private static async Task PrintResult(HttpResponseMessage response, bool verbose)
    {
        //TODO: refatorar
        var content = await response.Content.ReadAsStringAsync();

        if (verbose)
        {
            AnsiConsole.MarkupLine($"[bold yellow]< [/] {response.Version} {response.StatusCode}");
            AnsiConsole.MarkupLine($"[bold yellow]< Content-Type:[/] {response.Content.Headers.ContentType}");
            AnsiConsole.MarkupLine($"[bold yellow]< Date:[/] {response.Headers.Date}");
            AnsiConsole.MarkupLine($"[bold yellow]< Server:[/] {response.Headers.Server}");
            AnsiConsole.MarkupLine($"[bold yellow]< Transfer-Encoding:[/] {response.Headers.TransferEncoding}");
            AnsiConsole.WriteLine($"{content}");
        }
        else
            AnsiConsole.WriteLine($"{content}");
    }

    private static void PrintRequest(HttpRequestMessage message, bool verbose)
    {
        if (verbose)
        {
            AnsiConsole.MarkupLine($"[bold green]> [/] GET {message.RequestUri} {message.Version}");
            AnsiConsole.MarkupLine($"[bold green]> Host: [/] {message.RequestUri}");
            AnsiConsole.MarkupLine($"[bold green]> User-Agent: [/] curl/8.6.0");
            AnsiConsole.MarkupLine($"[bold green]> Accept: [/] {message.Headers.Accept}");
        }
    }
}

public sealed class GetSettings : BaseSettings
{
    [Description("URL to request")]
    [CommandArgument(1, "[url]")]
    public required string Url { private get; init; }

    public Uri Uri => new(Url.Trim());
}
