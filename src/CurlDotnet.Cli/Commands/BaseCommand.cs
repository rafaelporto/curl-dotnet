using System.ComponentModel;

namespace CurlDotnet.Commands;

public abstract class BaseSettings : CommandSettings
{
    [Description("Print the details of the request")]
    [CommandOption("-v|--verbose")]
    [DefaultValue(false)]
    public bool Verbose { get; init; }
}
