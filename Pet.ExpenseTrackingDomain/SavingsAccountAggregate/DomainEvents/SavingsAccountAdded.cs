using MediatR;

namespace Pet.ExpenseTracking.Domain.SavingsAccountAggregate.DomainEvents
{
    public record SavingsAccountAdded(string Iban): INotification;
}
