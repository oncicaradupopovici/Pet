using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.ExpenseTracking.Domain.IncomeAggregate
{
    public interface IIncomeRepository : IUowRepository<Income>
    {
        Task AddAsync(Income entity);
    }
}
