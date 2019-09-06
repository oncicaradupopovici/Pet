using NBB.Domain;
using Pet.ExpenseTracking.Domain.SavingsAccountAggregate.DomainEvents;

namespace Pet.ExpenseTracking.Domain.SavingsAccountAggregate
{
    public class SavingsAccount : EventedAggregateRoot<string>
    {
        public string Iban { get; private set; }

        private SavingsAccount()
        {
        }

        public SavingsAccount(string iban)
        {
            Iban = iban;
            AddEvent(new SavingsAccountAdded(Iban));
        }

        public override string GetIdentityValue() => Iban;
    }
}
