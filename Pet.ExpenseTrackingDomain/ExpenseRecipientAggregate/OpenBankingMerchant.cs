using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate
{
    public class OpenBankingMerchant : Entity<string>
    {
        public string Code { get; private set; }
        public Guid ExpenseRecipientId { get; private set; }

        //for ef
        private OpenBankingMerchant()
        {
        }

        internal OpenBankingMerchant(string code, Guid expenseRecipientId)
        {
            Code = code;
            ExpenseRecipientId = expenseRecipientId;
        }

        public override string GetIdentityValue() => Code;
    }
}
