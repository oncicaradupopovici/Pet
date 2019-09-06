using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NBB.Core.Abstractions;
using Pet.ExpenseTracking.Domain.SavingsAccountAggregate;

namespace Pet.ExpenseTracking.Data.Repositories
{
    class SavingsAccountRepository : ISavingsAccountRepository
    {
        private readonly ExpenseTrackingDbContext _dbContext;

        public SavingsAccountRepository(ExpenseTrackingDbContext dbContext, IUow<SavingsAccount> uow)
        {
            _dbContext = dbContext;
            Uow = uow;
        }

        public IUow<SavingsAccount> Uow { get; }

        public Task AddAsync(SavingsAccount entity)
        {
            return _dbContext.Set<SavingsAccount>().AddAsync(entity);
        }

        public Task<bool> IsSavingsAccount(string iban)
        {
            return _dbContext.Set<SavingsAccount>().AnyAsync(x => x.Iban == iban);
        }
    }
}
