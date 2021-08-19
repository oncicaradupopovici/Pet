using System;
using System.Threading.Tasks;
using NBB.Core.Abstractions;
using Pet.Banking.Domain.CashWithdrawalAggregate;

namespace Pet.Banking.Data.Repositories
{
    class CashWithdrawalRepository : ICashWithdrawalRepository
    {
        private readonly BankingDbContext _dbContext;

        public CashWithdrawalRepository(BankingDbContext dbContext, IUow<CashWithdrawal> uow)
        {
            _dbContext = dbContext;
            Uow = uow;
        }

        public IUow<CashWithdrawal> Uow { get; }
        public async Task AddAsync(CashWithdrawal entity) 
            => await _dbContext.Set<CashWithdrawal>().AddAsync(entity);

        public async Task<CashWithdrawal> FindById(Guid cashWithdrawalId)
            => await _dbContext.Set<CashWithdrawal>().FindAsync(cashWithdrawalId);
    }
}
