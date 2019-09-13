using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NBB.Core.Abstractions;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;

namespace Pet.ExpenseTracking.Data.Repositories
{
    class ExpenseRecipientRepository : IExpenseRecipientRepository
    {
        private readonly ExpenseTrackingDbContext _dbContext;

        public ExpenseRecipientRepository(ExpenseTrackingDbContext dbContext, IUow<ExpenseRecipient> uow)
        {
            _dbContext = dbContext;
            Uow = uow;
        }

        public IUow<ExpenseRecipient> Uow { get; }

        public Task AddAsync(ExpenseRecipient entity) => _dbContext.Set<ExpenseRecipient>().AddAsync(entity);

        public async Task<ExpenseRecipient> FindByPosTerminal(string posTerminalCode)
        {
            var posTerminal = await _dbContext.Set<PosTerminal>().SingleOrDefaultAsync(x => x.Code == posTerminalCode);
            if (posTerminal != null)
            {
                return await _dbContext.Set<ExpenseRecipient>().FindAsync(posTerminal.ExpenseRecipientId);
            }

            return null;
        }

        public Task<ExpenseRecipient> FindById(Guid expenseRecipientId)
        {
            return _dbContext.Set<ExpenseRecipient>().FindAsync(expenseRecipientId);
        }

        public async Task<ExpenseRecipient> FindBestMatchForPosTerminalCode(string posTerminalCode)
        {
            var expenseRecipients = await _dbContext.Set<ExpenseRecipient>().Where(x => EF.Functions.Like(posTerminalCode, x.PosTerminalMatchPattern)).ToListAsync();
            if (expenseRecipients.Count == 1)
            {
                return expenseRecipients.Single();
            }

            return null;
        }

        public async Task<ExpenseRecipient> FindByIban(string ibanCode)
        {
            var iban = await _dbContext.Set<Iban>().SingleOrDefaultAsync(x => x.Code == ibanCode);
            if (iban != null)
            {
                return await _dbContext.Set<ExpenseRecipient>().FindAsync(iban.ExpenseRecipientId);
            }

            return null;
        }

        public async Task<ExpenseRecipient> FindByDirectDebit(string directDebitCode)
        {
            var directDebit = await _dbContext.Set<DirectDebit>().SingleOrDefaultAsync(x => x.Code == directDebitCode);
            if (directDebit != null)
            {
                return await _dbContext.Set<ExpenseRecipient>().FindAsync(directDebit.ExpenseRecipientId);
            }

            return null;
        }

        public Task<List<ExpenseRecipient>> FindAll()
        {
            return _dbContext.Set<ExpenseRecipient>().ToListAsync();
        }
    }
}
