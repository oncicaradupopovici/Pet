using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.ExpenseTracking.Domain.SavingsAccountAggregate
{
    public interface ISavingsAccountRepository : IUowRepository<SavingsAccount>
    {
        Task AddAsync(SavingsAccount entity);
        Task<bool> IsSavingsAccount(string iban);
    }
}
