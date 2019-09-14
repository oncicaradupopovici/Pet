using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NBB.Core.Abstractions;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;

namespace Pet.ExpenseTracking.Data.Repositories
{
    class ExpenseRepository: IExpenseRepository
    {
        private readonly ExpenseTrackingDbContext _dbContext;
        public IUow<Expense> Uow { get; }

        public ExpenseRepository(IUow<Expense> uow, ExpenseTrackingDbContext dbContext)
        {
            Uow = uow;
            _dbContext = dbContext;
        }

        public Task<Expense> FindById(Guid id) => _dbContext.Set<Expense>().FindAsync(id);
        public Task<List<Expense>> FindByExpenseRecipientAndMonth(Guid expenseRecipientId, int expenseMonth)
        {
            return _dbContext.Set<Expense>().Where(x => x.ExpenseRecipientId == expenseRecipientId && x.ExpenseMonth == expenseMonth).ToListAsync();
        }

        public Task AddAsync(Expense entity) => _dbContext.Set<Expense>().AddAsync(entity);
        public Task UpdateAsync(Expense entity)
        {
            if (entity.IsDeleted())
            {
                _dbContext.Set<Expense>().Remove(entity);
            }
            else
            {
                _dbContext.Set<Expense>().Update(entity);
            }

            return Task.CompletedTask;
        }

        public async Task<List<Expense>> FindByPosTerminal(string posTerminalCode)
        {
            var expenses = await _dbContext.Set<Expense>()
                .Where(x => x.ExpenseType == ExpenseType.PosPayment && x.ExpenseSourceId.HasValue && x.ExpenseRecipientDetailCode == posTerminalCode)
                .ToListAsync();

            return expenses;
        }

        public async Task<List<Expense>> FindByOpenBankingMerchant(string merchant)
        {
            var expenses = await _dbContext.Set<Expense>()
                .Where(x => x.ExpenseType == ExpenseType.OpenBankingPayment && x.ExpenseSourceId.HasValue && x.ExpenseRecipientDetailCode == merchant)
                .ToListAsync();

            return expenses;
        }

        public async Task<List<Expense>> FindByIban(string iban)
        {
            var expenses = await _dbContext.Set<Expense>()
                .Where(x => x.ExpenseType == ExpenseType.BankTransfer && x.ExpenseSourceId.HasValue && x.ExpenseRecipientDetailCode == iban)
                .ToListAsync();

            return expenses;
        }

        public async Task<List<Expense>> FindBySourceCategory(string category)
        {
            var expenses = await _dbContext.Set<Expense>()
                .Where(x => x.ExpenseType == ExpenseType.OpenBankingPayment && x.ExpenseSourceId.HasValue && x.SourceCategory == category)
                .ToListAsync();

            return expenses;
        }

        public async Task<List<Expense>> FindByDirectDebitCode(string directDebitCode)
        {
            var expenses = await _dbContext.Set<Expense>()
                .Where(x => x.ExpenseType == ExpenseType.DirectDebitPayment && x.ExpenseSourceId.HasValue && x.ExpenseRecipientDetailCode == directDebitCode)
                .ToListAsync();

            return expenses;
        }

        public async Task RemoveAsync(object id)
        {
            var ids = (id is object[] list) ? list : new[] {id};

            var existingEntity = await _dbContext.Set<Expense>().FindAsync(ids);
            _dbContext.Set<Expense>().Remove(existingEntity);
        }
    }
}
