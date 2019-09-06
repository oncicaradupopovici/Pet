using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.ExpenseTracking.Domain.SavingsTransactionAggregate
{
    public interface ISavingsTransactionRepository : IUowRepository<SavingsTransaction>
    {
        Task AddAsync(SavingsTransaction entity);
    }
}
