using MediatR;
using NBB.Data.Abstractions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.Services;

namespace Pet.Application.DomainOrchestration
{
    public class ExpenseRecipientOrchestration :
        INotificationHandler<ExpenseRecipientCategoryChanged>
    {

        private readonly IExpenseRepository _expenseRepository;
        private readonly ExpenseService _expenseService;

        public ExpenseRecipientOrchestration(IExpenseRepository expenseRepository, ExpenseService expenseService)
        {
            _expenseRepository = expenseRepository;
            _expenseService = expenseService;
        }

        public async Task Handle(ExpenseRecipientCategoryChanged notification, CancellationToken cancellationToken)
        {
            var expenses = await _expenseService.UpdateExpensesWhen(notification);
            await Task.WhenAll(expenses.Select(_expenseRepository.UpdateAsync));

            await _expenseRepository.SaveChangesAsync();
        }
    }
}
