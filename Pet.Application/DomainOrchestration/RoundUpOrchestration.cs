using MediatR;
using NBB.Data.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.SavingsTransactionAggregate;
using Pet.ExpenseTracking.Domain.Services;
using Pet.Banking.Domain.RoundUpAggregate.DomainEvents;

namespace Pet.Application.DomainOrchestration
{
    public class RoundUpOrchestration :
        INotificationHandler<RoundUpAdded>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ExpenseService _expenseService;
        private readonly ISavingsTransactionRepository _savingsTransactionRepository;
        private readonly SavingsService _savingsService;

        public RoundUpOrchestration(IExpenseRepository expenseRepository, ExpenseService expenseService, ISavingsTransactionRepository savingsTransactionRepository, SavingsService savingsService)
        {
            _expenseRepository = expenseRepository;
            _expenseService = expenseService;
            _savingsTransactionRepository = savingsTransactionRepository;
            _savingsService = savingsService;
        }

        public async Task Handle(RoundUpAdded notification, CancellationToken cancellationToken)
        {
            var savingsTransaction = _savingsService.CreateSavingsTransactionWhen(notification);
            await _savingsTransactionRepository.AddAsync(savingsTransaction);
            await _savingsTransactionRepository.SaveChangesAsync();
        }
    }
}
