using System;
using System.Threading.Tasks;
using NBB.Core.Abstractions;
using Pet.Banking.Domain.ExchangeAggregate;

namespace Pet.Banking.Data.Repositories
{
    class ExchangeRepository : IExchangeRepository
    {
        private readonly BankingDbContext _dbContext;

        public ExchangeRepository(BankingDbContext dbContext, IUow<Exchange> uow)
        {
            _dbContext = dbContext;
            Uow = uow;
        }

        public IUow<Exchange> Uow { get; }
        public async Task AddAsync(Exchange entity) => await _dbContext.Set<Exchange>().AddAsync(entity);

        public async Task<Exchange> FindById(Guid exchangeId)
        {
            return await _dbContext.Set<Exchange>().FindAsync(exchangeId);
        }
    }
}
