using Microsoft.Extensions.DependencyInjection;

namespace CurlDotnet.Infrastructure;

public sealed class TypeRegistrar(IServiceCollection builder) : ITypeRegistrar
{
    private readonly IServiceCollection _builder = builder;

    public ITypeResolver Build() => new TypeResolver(_builder.BuildServiceProvider());

    public void Register(Type service, Type implementation) => _ = _builder.AddSingleton(service, implementation);

    public void RegisterInstance(Type service, object implementation) =>
        _ = _builder.AddSingleton(service, implementation);

    public void RegisterLazy(Type service, Func<object> factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _ = _builder.AddSingleton(service, (provider) => factory());
    }
}