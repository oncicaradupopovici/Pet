using System;
using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.Banking.Domain.ExchangeAggregate
{
    public interface IExchangeRepository : IUowRepository<Exchange>
    {
        Task AddAsync(Exchange entity);
        Task<Exchange> FindById(Guid exchangeId);
    }
}
