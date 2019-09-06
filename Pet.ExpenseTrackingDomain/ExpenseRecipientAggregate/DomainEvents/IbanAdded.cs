using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public class IbanAdded : DomainEvent
    {
        public string Iban { get; }
        public Guid ExpenseRecipientId { get; }

        public IbanAdded(string iban, Guid expenseRecipientId, DomainEventMetadata metadata = null) : base(metadata)
        {
            Iban = iban;
            ExpenseRecipientId = expenseRecipientId;
        }
    }
}
