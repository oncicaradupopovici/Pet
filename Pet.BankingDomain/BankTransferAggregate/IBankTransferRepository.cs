using System;
using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.Banking.Domain.BankTransferAggregate
{
    public interface IBankTransferRepository : IUowRepository<BankTransfer>
    {
        Task AddAsync(BankTransfer entity);
        Task<BankTransfer> FindById(Guid bankTransferId);
    }
}
