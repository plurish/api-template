using Polly.Contrib.WaitAndRetry;

namespace Plurish.Common.Configuration;

/// <summary>
/// Principais parâmetros de <see cref="Backoff.DecorrelatedJitterBackoffV2" />
/// </summary>
public sealed record ResilienceOptions(
    double MedianFirstRetryDelay,
    int RetryCount
);