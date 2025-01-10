using Shared.DomainEvents;

namespace Shared.Domain.Entities;

public interface IBaseEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvents();
}