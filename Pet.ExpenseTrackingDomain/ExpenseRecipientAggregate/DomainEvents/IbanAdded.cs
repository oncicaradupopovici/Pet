using System;
using MediatR;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public record IbanAdded(string Iban, Guid ExpenseRecipientId) : INotification;
}
