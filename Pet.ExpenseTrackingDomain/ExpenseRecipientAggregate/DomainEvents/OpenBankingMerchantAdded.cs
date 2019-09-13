using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents
{
    public class OpenBankingMerchantAdded : DomainEvent
    {
        public string Code { get; }
        public Guid ExpenseRecipientId { get; }

        public OpenBankingMerchantAdded(string code, Guid expenseRecipientId, DomainEventMetadata metadata = null) : base(metadata)
        {
            Code = code;
            ExpenseRecipientId = expenseRecipientId;
        }
    }
}
