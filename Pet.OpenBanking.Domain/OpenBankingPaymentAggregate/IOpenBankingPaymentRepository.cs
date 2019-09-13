using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NBB.Data.Abstractions;

namespace Pet.OpenBanking.Domain.OpenBankingPaymentAggregate
{
    public interface IOpenBankingPaymentRepository : IUowRepository<OpenBankingPayment>
    {
        Task AddAsync(OpenBankingPayment entity);
        Task<List<OpenBankingPayment>> FindByMerchant(string merchant);
        Task<OpenBankingPayment> FindById(Guid openBankingPaymentId);
    }
}
