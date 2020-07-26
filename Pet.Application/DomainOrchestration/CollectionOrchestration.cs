using MediatR;
using NBB.Data.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Pet.ExpenseTracking.Domain.SavingsTransactionAggregate;
using Pet.ExpenseTracking.Domain.Services;
using Pet.Banking.Domain.RoundUpAggregate.DomainEvents;
using Pet.Banking.Domain.CollectionAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.IncomeAggregate;

namespace Pet.Application.DomainOrchestration
{
    public class CollectionOrchestration :
        INotificationHandler<CollectionAdded>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ISavingsTransactionRepository _savingsTransactionRepository;
        private readonly IncomeService _incomeService;
        private readonly SavingsService _savingsService;

        public CollectionOrchestration(IIncomeRepository incomeRepository, ISavingsTransactionRepository savingsTransactionRepository, IncomeService incomeService, SavingsService savingsService)
        {
            _incomeRepository = incomeRepository;
            _savingsTransactionRepository = savingsTransactionRepository;
            _incomeService = incomeService;
            _savingsService = savingsService;
        }

        public async Task Handle(CollectionAdded notification, CancellationToken cancellationToken)
        {
            var savingsTransaction = await _savingsService.CreateSavingsTransactionWhen(notification);
            if (savingsTransaction != null)
            {
                await _savingsTransactionRepository.AddAsync(savingsTransaction);
                await _savingsTransactionRepository.SaveChangesAsync();
            }
            else
            {
                var income = await _incomeService.CreateIncomeWhen(notification);
                if(income != null)
                {
                    await _incomeRepository.AddAsync(income);
                    await _incomeRepository.SaveChangesAsync();
                }
            }
        }
    }
}
