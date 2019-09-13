using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NBB.Core.Abstractions;
using NBB.Data.Abstractions;
using Pet.OpenBanking.Domain.OpenBankingPaymentAggregate;

namespace Pet.OpenBanking.Data.Repositories
{
    class OpenBankingPaymentRepository : IOpenBankingPaymentRepository
    {
        private readonly ICrudRepository<OpenBankingPayment> _crudRepository;
        private readonly OpenBankingDbContext _dbContext;

        public OpenBankingPaymentRepository(ICrudRepository<OpenBankingPayment> crudRepository, OpenBankingDbContext dbContext)
        {
            _crudRepository = crudRepository;
            _dbContext = dbContext;
        }

        public IUow<OpenBankingPayment> Uow => _crudRepository.Uow;
        public Task AddAsync(OpenBankingPayment entity) => _crudRepository.AddAsync(entity);

        public Task<OpenBankingPayment> FindById(Guid openBankingPaymentId)
        {
            return _dbContext.Set<OpenBankingPayment>().FindAsync(openBankingPaymentId);
        }

        public Task<List<OpenBankingPayment>> FindByMerchant(string merchant)
        {
            return _dbContext.Set<OpenBankingPayment>().Where(x => x.Merchant == merchant).ToListAsync();
        }
    }
}
