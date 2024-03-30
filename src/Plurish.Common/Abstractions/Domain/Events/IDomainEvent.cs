using MediatR;

namespace Plurish.Common.Abstractions.Domain.Events;

/// <summary>
/// Marker interface para representação de um domain event
/// </summary>
public interface IDomainEvent : INotification;