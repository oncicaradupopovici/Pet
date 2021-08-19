using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NBB.Core.Abstractions;
using NBB.Data.Abstractions;
using Pet.Banking.Domain.PosPaymentAggregate;

namespace Pet.Banking.Data.Repositories
{
    class PosPaymentRepository : IPosPaymentRepository
    {
        private readonly ICrudRepository<PosPayment> _crudRepository;
        private readonly BankingDbContext _dbContext;

        public PosPaymentRepository(ICrudRepository<PosPayment> crudRepository, BankingDbContext dbContext)
        {
            _crudRepository = crudRepository;
            _dbContext = dbContext;
        }

        public IUow<PosPayment> Uow => _crudRepository.Uow;
        public Task AddAsync(PosPayment entity) => _crudRepository.AddAsync(entity);

        public async Task<PosPayment> FindById(Guid posPaymentId)
        {
            return await _dbContext.Set<PosPayment>().FindAsync(posPaymentId);
        }

        public Task<List<PosPayment>> FindByPosTerminalCode(string posTerminalCode)
        {
            return _dbContext.Set<PosPayment>().Where(x => x.PosTerminalCode == posTerminalCode).ToListAsync();
        }
    }
}
