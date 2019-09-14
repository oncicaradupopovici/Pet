using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NBB.Core.Abstractions;
using Pet.ExpenseTracking.Domain.SavingsCategoryAggregate;

namespace Pet.ExpenseTracking.Data.Repositories
{
    class SavingsCategoryRepository : ISavingsCategoryRepository
    {
        private readonly ExpenseTrackingDbContext _dbContext;

        public SavingsCategoryRepository(ExpenseTrackingDbContext dbContext, IUow<SavingsCategory> uow)
        {
            _dbContext = dbContext;
            Uow = uow;
        }

        public IUow<SavingsCategory> Uow { get; }

        public Task AddAsync(SavingsCategory entity)
        {
            return _dbContext.Set<SavingsCategory>().AddAsync(entity);
        }

        public Task<bool> IsSavingsCategory(string category)
        {
            return _dbContext.Set<SavingsCategory>().AnyAsync(x => x.Category == category);
        }
    }
}
