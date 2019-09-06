using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NBB.Core.Abstractions;
using NBB.Data.Abstractions;
using Pet.ExpenseTracking.Domain.ExpenseCategoryAggregate;

namespace Pet.ExpenseTracking.Data.Repositories
{
    class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly ICrudRepository<ExpenseCategory> _crudRepository;
        private readonly ExpenseTrackingDbContext _dbContext;

        public ExpenseCategoryRepository(ICrudRepository<ExpenseCategory> crudRepository, ExpenseTrackingDbContext dbContext)
        {
            _crudRepository = crudRepository;
            _dbContext = dbContext;
        }

        public IUow<ExpenseCategory> Uow => _crudRepository.Uow;

        public Task<List<ExpenseCategory>> FindAll()
        {
            return _dbContext.Set<ExpenseCategory>().ToListAsync();
        }

        public Task AddAsync(ExpenseCategory entity) => _crudRepository.AddAsync(entity);
    }
}
