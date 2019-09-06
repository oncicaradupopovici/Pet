using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.Banking.Domain.DirectDebitPaymentAggregate
{
    public interface IDirectDebitPaymentRepository : IUowRepository<DirectDebitPayment>
    {
        Task AddAsync(DirectDebitPayment entity);
        Task<List<DirectDebitPayment>> FindByDirectDebitCode(string directDebitCode);
        Task<DirectDebitPayment> FindById(Guid directDebitPaymentId);
    }
}
