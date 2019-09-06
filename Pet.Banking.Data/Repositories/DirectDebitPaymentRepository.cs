using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NBB.Core.Abstractions;
using NBB.Data.Abstractions;
using Pet.Banking.Domain.DirectDebitPaymentAggregate;

namespace Pet.Banking.Data.Repositories
{
    class DirectDebitPaymentRepository : IDirectDebitPaymentRepository
    {
        private readonly ICrudRepository<DirectDebitPayment> _crudRepository;
        private readonly BankingDbContext _dbContext;

        public DirectDebitPaymentRepository(ICrudRepository<DirectDebitPayment> crudRepository, BankingDbContext dbContext)
        {
            _crudRepository = crudRepository;
            _dbContext = dbContext;
        }

        public IUow<DirectDebitPayment> Uow => _crudRepository.Uow;
        public Task AddAsync(DirectDebitPayment entity) => _crudRepository.AddAsync(entity);

        public Task<DirectDebitPayment> FindById(Guid posPaymentId)
        {
            return _dbContext.Set<DirectDebitPayment>().FindAsync(posPaymentId);
        }

        public Task<List<DirectDebitPayment>> FindByDirectDebitCode(string directDebitCode)
        {
            return _dbContext.Set<DirectDebitPayment>().Where(x => x.DirectDebitCode == directDebitCode).ToListAsync();
        }
    }
}
