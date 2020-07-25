using System;
using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.Banking.Domain.RoundUpAggregate
{
    public interface IRoundUpRepository : IUowRepository<RoundUp>
    {
        Task AddAsync(RoundUp entity);
        Task<RoundUp> FindById(Guid roundUpId);
    }
}
