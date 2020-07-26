using System.Threading.Tasks;
using NBB.Core.Abstractions;
using Pet.ExpenseTracking.Domain.IncomeAggregate;

namespace Pet.ExpenseTracking.Data.Repositories
{
    class IncomeRepository : IIncomeRepository
    {
        private readonly ExpenseTrackingDbContext _dbContext;

        public IncomeRepository(ExpenseTrackingDbContext dbContext, IUow<Income> uow)
        {
            _dbContext = dbContext;
            Uow = uow;
        }

        public IUow<Income> Uow { get; }

        public Task AddAsync(Income entity)
        {
            return _dbContext.Set<Income>().AddAsync(entity);
        }
    }
}
