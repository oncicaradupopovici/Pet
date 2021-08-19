using System;
using MediatR;

namespace Pet.Banking.Domain.CollectionAggregate.DomainEvents
{
    public record CollectionAdded(Guid CollectionId, string From, string FromIban, string Details, decimal Value,
        DateTime IncomeDate) : INotification;
}
