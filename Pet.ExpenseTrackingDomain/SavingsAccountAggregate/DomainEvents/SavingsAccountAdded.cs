using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.SavingsAccountAggregate.DomainEvents
{
    public class SavingsAccountAdded : DomainEvent
    {
        public string Iban { get; }

        public SavingsAccountAdded(string iban, DomainEventMetadata metadata = null) : base(metadata)
        {
            Iban = iban;
        }
    }
}
