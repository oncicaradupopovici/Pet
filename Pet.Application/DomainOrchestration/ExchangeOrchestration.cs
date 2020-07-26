using MediatR;
using NBB.Data.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Pet.ExpenseTracking.Domain.SavingsTransactionAggregate;
using Pet.ExpenseTracking.Domain.Services;
using Pet.Banking.Domain.ExchangeAggregate.DomainEvents;

namespace Pet.Application.DomainOrchestration
{
    public class ExchangeOrchestration :
        INotificationHandler<ExchangeAdded>
    {
        private readonly ISavingsTransactionRepository _savingsTransactionRepository;
        private readonly SavingsService _savingsService;

        public ExchangeOrchestration(ISavingsTransactionRepository savingsTransactionRepository, SavingsService savingsService)
        {
            _savingsTransactionRepository = savingsTransactionRepository;
            _savingsService = savingsService;
        }

        public async Task Handle(ExchangeAdded notification, CancellationToken cancellationToken)
        {
            var savingsTransaction = _savingsService.CreateSavingsTransactionWhen(notification);
            await _savingsTransactionRepository.AddAsync(savingsTransaction);
            await _savingsTransactionRepository.SaveChangesAsync();
        }
    }
}
