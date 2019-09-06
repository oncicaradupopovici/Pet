using System.Threading.Tasks;
using Pet.Banking.Domain.PosPaymentAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;

namespace Pet.ExpenseTracking.Domain.Services
{
    public class ExpenseRecipientService
    {
        private readonly IExpenseRecipientRepository _expenseRecipientRepository;

        public ExpenseRecipientService(IExpenseRecipientRepository expenseRecipientRepository)
        {
            _expenseRecipientRepository = expenseRecipientRepository;
        }

        public async Task<ExpenseRecipient> AddPosTerminalWhen(PosPaymentAdded notification)
        {
            var existingExpenseRecipientForPosTerminal = await _expenseRecipientRepository.FindByPosTerminal(notification.PosTerminalCode);
            if (existingExpenseRecipientForPosTerminal != null)
            {
                return null;
            }

            var expenseRecipient =
                await _expenseRecipientRepository.FindBestMatchForPosTerminalCode(notification.PosTerminalCode);

            if (expenseRecipient != null)
            {
                expenseRecipient.AddPosTerminal(notification.PosTerminalCode);
                return expenseRecipient;
            }

            return null;
        }
    }
}
