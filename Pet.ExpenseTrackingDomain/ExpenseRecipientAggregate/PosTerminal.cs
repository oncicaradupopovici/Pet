﻿using System;
using NBB.Domain;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate
{
    public class PosTerminal : Entity<string>
    {
        public string Code { get; private set; }
        public Guid ExpenseRecipientId { get; private set; }

        //for ef
        private PosTerminal()
        {
        }

        internal PosTerminal(string code, Guid expenseRecipientId)
        {
            Code = code;
            ExpenseRecipientId = expenseRecipientId;
        }

        public override string GetIdentityValue() => Code;
    }
}
