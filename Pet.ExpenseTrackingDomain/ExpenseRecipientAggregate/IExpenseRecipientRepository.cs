using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate
{
    public interface IExpenseRecipientRepository : IUowRepository<ExpenseRecipient>
    {
        Task AddAsync(ExpenseRecipient entity);
        Task<ExpenseRecipient> FindByPosTerminal(string posTerminalCode);
        Task<ExpenseRecipient> FindByIban(string iban);
        Task<ExpenseRecipient> FindByDirectDebit(string directDebitCode);
        Task<ExpenseRecipient> FindBestMatchForPosTerminalCode(string posTerminalCode);
        Task<ExpenseRecipient> FindById(Guid expenseRecipientId);
        Task<List<ExpenseRecipient>> FindAll();

    }
}
