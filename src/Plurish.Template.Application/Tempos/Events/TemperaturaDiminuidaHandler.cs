using Microsoft.Extensions.Logging;
using Plurish.Common.Abstractions.Domain.Events;
using Plurish.Template.Domain.Tempos;

namespace Plurish.Template.Application.Tempos.Events;

internal sealed class TemperaturaDiminuidaHandler(
    ILogger<TemperaturaDiminuidaHandler> logger
) : DomainEventHandler<TemperaturaDiminuida>(logger)
{
    readonly ILogger<TemperaturaDiminuidaHandler> _logger = logger;

    protected override Task Execute(TemperaturaDiminuida @event, CancellationToken cancellationToken)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("[Handle] Evento recebido: {@Evento}", @event);
        }

        return Task.CompletedTask;
    }
}