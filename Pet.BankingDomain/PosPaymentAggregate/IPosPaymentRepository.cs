using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.Banking.Domain.PosPaymentAggregate
{
    public interface IPosPaymentRepository : IUowRepository<PosPayment>
    {
        Task AddAsync(PosPayment entity);
        Task<List<PosPayment>> FindByPosTerminalCode(string posTerminalCode);
        Task<PosPayment> FindById(Guid posPaymentId);
    }
}
