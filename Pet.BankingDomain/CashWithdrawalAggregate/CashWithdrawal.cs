using System;
using NBB.Domain;
using Pet.Banking.Domain.CashWithdrawalAggregate.DomainEvents;

namespace Pet.Banking.Domain.CashWithdrawalAggregate
{
    public class CashWithdrawal : EventedAggregateRoot<Guid>
    {
        public Guid CashWithdrawalId { get; private set;}
        public string CashTerminal { get; private set;}
        public decimal Value { get; private set;}
        public DateTime WithdrawalDate { get; private set;}

        //for ef
        private CashWithdrawal()
        {

        }

        public CashWithdrawal(string cashTerminal, decimal value, DateTime withdrawalDate)
        {
            CashWithdrawalId = Guid.NewGuid();
            CashTerminal = cashTerminal;
            Value = value;
            WithdrawalDate = withdrawalDate;

            AddEvent(new CashWithdrawalAdded(CashWithdrawalId, CashTerminal, Value, WithdrawalDate));

        }

        public override Guid GetIdentityValue() => CashWithdrawalId;
    }
}
