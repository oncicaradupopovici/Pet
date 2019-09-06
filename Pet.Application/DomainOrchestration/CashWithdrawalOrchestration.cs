using MediatR;
using NBB.Data.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Pet.Banking.Domain.CashWithdrawalAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.Services;

namespace Pet.Application.DomainOrchestration
{
    public class CashWithdrawalOrchestration :
        INotificationHandler<CashWithdrawalAdded>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ExpenseService _expenseService;

        public CashWithdrawalOrchestration(IExpenseRepository expenseRepository, ExpenseService expenseService)
        {
            _expenseRepository = expenseRepository;
            _expenseService = expenseService;
        }

        public async Task Handle(CashWithdrawalAdded notification, CancellationToken cancellationToken)
        {
            var expense = await _expenseService.CreateExpenseWhen(notification);
            if (expense != null)
            {
                await _expenseRepository.AddAsync(expense);
                await _expenseRepository.SaveChangesAsync();
            }
        }
    }
}
