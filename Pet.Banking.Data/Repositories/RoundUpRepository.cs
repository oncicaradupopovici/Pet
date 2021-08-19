using System;
using System.Threading.Tasks;
using NBB.Core.Abstractions;
using Pet.Banking.Domain.RoundUpAggregate;

namespace Pet.Banking.Data.Repositories
{
    class RoundUpRepository : IRoundUpRepository
    {
        private readonly BankingDbContext _dbContext;

        public RoundUpRepository(BankingDbContext dbContext, IUow<RoundUp> uow)
        {
            _dbContext = dbContext;
            Uow = uow;
        }

        public IUow<RoundUp> Uow { get; }
        public async Task AddAsync(RoundUp entity) => await _dbContext.Set<RoundUp>().AddAsync(entity);

        public async Task<RoundUp> FindById(Guid roundUpId)
        {
            return await _dbContext.Set<RoundUp>().FindAsync(roundUpId);
        }
    }
}
