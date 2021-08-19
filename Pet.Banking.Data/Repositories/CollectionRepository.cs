using System;
using System.Threading.Tasks;
using NBB.Core.Abstractions;
using Pet.Banking.Domain.CollectionAggregate;

namespace Pet.Banking.Data.Repositories
{
    class CollectionRepository : ICollectionRepository
    {
        private readonly BankingDbContext _dbContext;

        public CollectionRepository(BankingDbContext dbContext, IUow<Collection> uow)
        {
            _dbContext = dbContext;
            Uow = uow;
        }

        public IUow<Collection> Uow { get; }
        public async Task AddAsync(Collection entity) => await _dbContext.Set<Collection>().AddAsync(entity);

        public async Task<Collection> FindById(Guid collectionId)
        {
            return await _dbContext.Set<Collection>().FindAsync(collectionId);
        }
    }
}
