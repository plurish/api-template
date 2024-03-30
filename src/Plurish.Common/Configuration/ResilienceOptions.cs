using Polly.Contrib.WaitAndRetry;

namespace Plurish.Common.Configuration;

/// <summary>
/// Principais parâmetros de <see cref="Backoff.DecorrelatedJitterBackoffV2" />
/// </summary>
public sealed record ResilienceOptions
{
    public required double MedianFirstRetryDelay { get; init; }
    public required int RetryCount { get; init; }
}