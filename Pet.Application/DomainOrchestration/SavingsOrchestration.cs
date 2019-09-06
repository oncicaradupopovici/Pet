using MediatR;
using NBB.Data.Abstractions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.SavingsAccountAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.SavingsTransactionAggregate;
using Pet.ExpenseTracking.Domain.Services;

namespace Pet.Application.DomainOrchestration
{
    public class SavingsOrchestration :
        INotificationHandler<SavingsAccountAdded>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ISavingsTransactionRepository _savingsTransactionRepository;
        private readonly SavingsService _savingsService;

        public SavingsOrchestration(IExpenseRepository expenseRepository, ISavingsTransactionRepository savingsTransactionRepository, SavingsService savingsService)
        {
            _expenseRepository = expenseRepository;
            _savingsTransactionRepository = savingsTransactionRepository;
            _savingsService = savingsService;
        }

        public async Task Handle(SavingsAccountAdded notification, CancellationToken cancellationToken)
        {
            var savings = await _savingsService.CreateSavingsTransactionsWhen(notification);
            await Task.WhenAll(savings.Select(_savingsTransactionRepository.AddAsync));
            await _savingsTransactionRepository.SaveChangesAsync();

            var expenses = await _expenseRepository.FindByIban(notification.Iban);
            await Task.WhenAll(expenses.Select(x =>
            {
                x.Delete();
                return _expenseRepository.UpdateAsync(x);
            }));
            await _expenseRepository.SaveChangesAsync();

        }
    }
}
