using System.Collections.Generic;
using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.ExpenseTracking.Domain.ExpenseCategoryAggregate
{
    public interface IExpenseCategoryRepository : IUowRepository<ExpenseCategory>
    {
        Task<List<ExpenseCategory>> FindAll();
        Task AddAsync(ExpenseCategory entity);
    }
}
