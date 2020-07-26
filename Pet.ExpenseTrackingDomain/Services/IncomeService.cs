using System.Threading.Tasks;
using Pet.Banking.Domain.CollectionAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.IncomeAggregate;
using Pet.ExpenseTracking.Domain.SavingsAccountAggregate;

namespace Pet.ExpenseTracking.Domain.Services
{
    public class IncomeService
    {
        private readonly IncomeFactory _incomeFactory;
        private readonly ISavingsAccountRepository _savingsAccountRepository;

        public IncomeService(IncomeFactory incomeFactory, ISavingsAccountRepository savingsAccountRepository)
        {
            _incomeFactory = incomeFactory;
            _savingsAccountRepository = savingsAccountRepository;
        }

        public async Task<Income> CreateIncomeWhen(CollectionAdded notification)
        {
            var isSavingsAccount =
                await _savingsAccountRepository.IsSavingsAccount(notification.FromIban);

            if (isSavingsAccount)
                return null;

            var income = _incomeFactory.CreateFrom(IncomeType.Collection, notification.Value, notification.IncomeDate, notification.From, notification.FromIban, notification.Details, notification.CollectionId);
            return income;
        }

    }
}
