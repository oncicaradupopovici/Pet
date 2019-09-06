using System;
using NBB.Domain;

namespace Pet.Banking.Domain.CashWithdrawalAggregate.DomainEvents
{
    public class CashWithdrawalAdded : DomainEvent
    {
        public Guid CashWithdrawalId { get; }
        public string CashTerminal { get; }
        public decimal Value { get; }
        public DateTime WithdrawalDate { get; }

        public CashWithdrawalAdded(Guid cashWithdrawalId, string cashTerminal, decimal value, DateTime withdrawalDate, DomainEventMetadata metadata = null)
            : base(metadata)
        {
            CashWithdrawalId = cashWithdrawalId;
            CashTerminal = cashTerminal;
            Value = value;
            WithdrawalDate = withdrawalDate;
        }
    }
}
