using System;
using System.Threading.Tasks;
using NBB.Core.Abstractions;
using Pet.Banking.Domain.BankTransferAggregate;

namespace Pet.Banking.Data.Repositories
{
    class BankTransferRepository : IBankTransferRepository
    {
        private readonly BankingDbContext _dbContext;

        public BankTransferRepository(BankingDbContext dbContext, IUow<BankTransfer> uow)
        {
            _dbContext = dbContext;
            Uow = uow;
        }

        public IUow<BankTransfer> Uow { get; }
        public Task AddAsync(BankTransfer entity) => _dbContext.Set<BankTransfer>().AddAsync(entity);

        public Task<BankTransfer> FindById(Guid bankTransferId)
        {
            return _dbContext.Set<BankTransfer>().FindAsync(bankTransferId);
        }
    }
}
