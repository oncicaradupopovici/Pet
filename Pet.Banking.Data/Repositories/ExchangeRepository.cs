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
        public Task AddAsync(Exchange entity) => _dbContext.Set<Exchange>().AddAsync(entity);

        public Task<Exchange> FindById(Guid exchangeId)
        {
            return _dbContext.Set<Exchange>().FindAsync(exchangeId);
        }
    }
}
