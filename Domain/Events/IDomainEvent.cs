// ============================================================================
// IDomainEvent.cs - Domain Event Interface
// ============================================================================
//
// Purpose: Defines the contract for domain events in the system, enabling
// decoupled communication between domain components and supporting the
// implementation of event-driven architecture patterns.
//
// Features:
// - Marker interface for domain events
// - Timestamp tracking for event occurrence
// - Event correlation and tracing support
// - Type-safe event handling foundation
//
// Usage:
// - Implement this interface for all domain events
// - Use with DomainEventDispatcher for event publishing
// - Enables loose coupling between domain components
//
// ============================================================================

namespace DV.Shared.Domain.Events;

/// <summary>
/// Marker interface for all domain events in the system.
/// Domain events represent something important that happened in the domain
/// and can trigger side effects or notify other parts of the system.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Unique identifier for this event instance
    /// </summary>
    Guid EventId { get; }

    /// <summary>
    /// Timestamp when the domain event occurred
    /// </summary>
    DateTime OccurredAt { get; }

    /// <summary>
    /// Optional correlation ID for tracing related events
    /// </summary>
    string? CorrelationId { get; }

    /// <summary>
    /// The version of the event schema for compatibility
    /// </summary>
    int Version { get; }
}

/// <summary>
/// Base implementation of IDomainEvent with common properties
/// </summary>
public abstract class DomainEventBase : IDomainEvent
{
    protected DomainEventBase()
    {
        EventId = Guid.NewGuid();
        OccurredAt = DateTime.UtcNow;
        Version = 1;
    }

    protected DomainEventBase(string? correlationId) : this()
    {
        CorrelationId = correlationId;
    }

    public Guid EventId { get; }
    public DateTime OccurredAt { get; }
    public string? CorrelationId { get; }
    public virtual int Version { get; } = 1;
}

/// <summary>
/// Interface for handling domain events
/// </summary>
/// <typeparam name="TEvent">Type of domain event to handle</typeparam>
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    /// <summary>
    /// Handle the domain event
    /// </summary>
    /// <param name="domainEvent">The event to handle</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task representing the handling operation</returns>
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}