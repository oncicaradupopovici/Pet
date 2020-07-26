using System;
using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.Banking.Domain.CollectionAggregate
{
    public interface ICollectionRepository : IUowRepository<Collection>
    {
        Task AddAsync(Collection entity);
        Task<Collection> FindById(Guid collectionId);
    }
}
