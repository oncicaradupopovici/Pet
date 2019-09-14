using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.ExpenseTracking.Domain.SavingsCategoryAggregate
{
    public interface ISavingsCategoryRepository : IUowRepository<SavingsCategory>
    {
        Task AddAsync(SavingsCategory entity);
        Task<bool> IsSavingsCategory(string category);
    }
}
