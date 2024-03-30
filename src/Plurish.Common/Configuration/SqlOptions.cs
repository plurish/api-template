namespace Plurish.Common.Configuration;

/// <summary>
/// Seção individual de um banco SQL no appsettings
/// </summary>
public sealed class SqlOptions
{
    public required string ConnectionString { get; init; }
    public ResilienceOptions Resilience { get; init; } = default!;
}