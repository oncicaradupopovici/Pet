using MediatR;
using NBB.Data.Abstractions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pet.Banking.Domain.DirectDebitPaymentAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.Services;

namespace Pet.Application.DomainOrchestration
{
    public class DirectDebitOrchestration :
        INotificationHandler<DirectDebitPaymentAdded>,
        INotificationHandler<DirectDebitAdded>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ExpenseService _expenseService;

        public DirectDebitOrchestration(IExpenseRepository expenseRepository, ExpenseService expenseService)
        {
            _expenseRepository = expenseRepository;
            _expenseService = expenseService;
        }

        public async Task Handle(DirectDebitPaymentAdded notification, CancellationToken cancellationToken)
        {
            var expense = await _expenseService.CreateExpenseWhen(notification);
            if (expense != null)
            {
                await _expenseRepository.AddAsync(expense);
                await _expenseRepository.SaveChangesAsync();
            }
        }

        public async Task Handle(DirectDebitAdded notification, CancellationToken cancellationToken)
        {
            var expenses = await _expenseService.UpdateExpensesWhen(notification);
            await Task.WhenAll(expenses.Select(_expenseRepository.UpdateAsync));

            await _expenseRepository.SaveChangesAsync();
        }

    }
}
