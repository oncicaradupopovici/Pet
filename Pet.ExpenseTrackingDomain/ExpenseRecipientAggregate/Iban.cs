﻿using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate
{
    public class Iban : Entity<string>
    {
        public string Code { get; private set; }
        public Guid ExpenseRecipientId { get; private set; }

        //for ef
        private Iban()
        {
        }

        internal Iban(string code, Guid expenseRecipientId)
        {
            Code = code;
            ExpenseRecipientId = expenseRecipientId;
        }

        public override string GetIdentityValue() => Code;
    }
}
