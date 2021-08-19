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
        public async Task AddAsync(BankTransfer entity) => await _dbContext.Set<BankTransfer>().AddAsync(entity);

        public async Task<BankTransfer> FindById(Guid bankTransferId)
            => await _dbContext.Set<BankTransfer>().FindAsync(bankTransferId);
    }
}
