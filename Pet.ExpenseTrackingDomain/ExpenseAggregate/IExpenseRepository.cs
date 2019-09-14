using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.ExpenseTracking.Domain.ExpenseAggregate
{
    public interface IExpenseRepository : IUowRepository<Expense>
    {
        Task<Expense> FindById(Guid id);

        Task<List<Expense>> FindByExpenseRecipientAndMonth(Guid expenseRecipientId, int expenseMonth);
        Task<List<Expense>> FindByPosTerminal(string posTerminal);
        Task<List<Expense>> FindByOpenBankingMerchant(string merchant);
        Task<List<Expense>> FindByIban(string iban);
        Task<List<Expense>> FindBySourceCategory(string category);
        Task<List<Expense>> FindByDirectDebitCode(string directDebitCode);
        Task AddAsync(Expense entity);
        Task UpdateAsync(Expense entity);
    }
}
