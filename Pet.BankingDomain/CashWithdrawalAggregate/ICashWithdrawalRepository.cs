using System;
using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.Banking.Domain.CashWithdrawalAggregate
{
    public interface ICashWithdrawalRepository : IUowRepository<CashWithdrawal>
    {
        Task AddAsync(CashWithdrawal entity);
        Task<CashWithdrawal> FindById(Guid cashWithdrawalId);
    }
}
