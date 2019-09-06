using MediatR;
using NBB.Data.Abstractions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pet.Banking.Domain.BankTransferAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.SavingsTransactionAggregate;
using Pet.ExpenseTracking.Domain.Services;

namespace Pet.Application.DomainOrchestration
{
    public class BankTransferOrchestration :
        INotificationHandler<BankTransferAdded>,
        INotificationHandler<IbanAdded>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ExpenseService _expenseService;
        private readonly ISavingsTransactionRepository _savingsTransactionRepository;
        private readonly SavingsService _savingsService;

        public BankTransferOrchestration(IExpenseRepository expenseRepository, ExpenseService expenseService, ISavingsTransactionRepository savingsTransactionRepository, SavingsService savingsService)
        {
            _expenseRepository = expenseRepository;
            _expenseService = expenseService;
            _savingsTransactionRepository = savingsTransactionRepository;
            _savingsService = savingsService;
        }

        public async Task Handle(BankTransferAdded notification, CancellationToken cancellationToken)
        {
            var savingsTransaction = await _savingsService.CreateSavingsTransactionWhen(notification);
            if (savingsTransaction != null)
            {
                await _savingsTransactionRepository.AddAsync(savingsTransaction);
                await _savingsTransactionRepository.SaveChangesAsync();
            }
            else
            {
                var expense = await _expenseService.CreateExpenseWhen(notification);
                if (expense != null)
                {
                    await _expenseRepository.AddAsync(expense);
                    await _expenseRepository.SaveChangesAsync();
                }
            }
        }

        public async Task Handle(IbanAdded notification, CancellationToken cancellationToken)
        {
            var expenses = await _expenseService.UpdateExpensesWhen(notification);
            await Task.WhenAll(expenses.Select(_expenseRepository.UpdateAsync));

            await _expenseRepository.SaveChangesAsync();
        }
    }
}
