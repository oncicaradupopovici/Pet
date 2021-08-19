using MediatR;

namespace Pet.ExpenseTracking.Domain.ExpenseCategoryAggregate.DomainEvents
{
    public record ExpenseCategoryAdded(int ExpenseCategoryId, string Name) : INotification;
}
