﻿using System.Threading.Tasks;
using NBB.Core.Abstractions;
using Pet.ExpenseTracking.Domain.SavingsTransactionAggregate;

namespace Pet.ExpenseTracking.Data.Repositories
{
    class SavingsTransactionRepository : ISavingsTransactionRepository
    {
        private readonly ExpenseTrackingDbContext _dbContext;

        public SavingsTransactionRepository(ExpenseTrackingDbContext dbContext, IUow<SavingsTransaction> uow)
        {
            _dbContext = dbContext;
            Uow = uow;
        }

        public IUow<SavingsTransaction> Uow { get; }

        public Task AddAsync(SavingsTransaction entity)
        {
            return _dbContext.Set<SavingsTransaction>().AddAsync(entity);
        }
    }
}
