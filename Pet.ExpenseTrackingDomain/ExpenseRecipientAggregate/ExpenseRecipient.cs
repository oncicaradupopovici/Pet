using System;
using System.Collections.Generic;
using NBB.Domain;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents;

namespace Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate
{
    public class ExpenseRecipient : EventedAggregateRoot<Guid>
    {
        public Guid ExpenseRecipientId { get; private set; }
        public string Name { get; private set; }
        public int? ExpenseCategoryId { get; private set; }
        public string PosTerminalMatchPattern { get; private set; }

        public List<PosTerminal> PosTerminals { get; private set; }
        public List<Iban> Ibans { get; private set; }
        public List<DirectDebit> DirectDebits { get; private set; }
        public List<OpenBankingMerchant> OpenBankingMerchants { get; private set; }

        //for ef
        private ExpenseRecipient()
        {
            PosTerminals = new List<PosTerminal>();
            Ibans = new List<Iban>();
            DirectDebits = new List<DirectDebit>();
            OpenBankingMerchants = new List<OpenBankingMerchant>();
        }

        public ExpenseRecipient(string name)
            : this()
        {
            ExpenseRecipientId = Guid.NewGuid();
            Name = name;

            AddEvent(new ExpenseRecipientAdded(ExpenseRecipientId, Name));
        }

        public void ChangeExpenseCategory(int? expenseCategoryId, int expenseMonth)
        {
            if (ExpenseCategoryId != expenseCategoryId)
            {
                AddEvent(new ExpenseRecipientCategoryChanged(ExpenseRecipientId, ExpenseCategoryId, expenseCategoryId, expenseMonth));
                ExpenseCategoryId = expenseCategoryId;
            }
        }

        public void ChangePosTerminalMatchPattern(string posTerminalMatchPattern)
        {
            if (PosTerminalMatchPattern != posTerminalMatchPattern)
            {
                PosTerminalMatchPattern = posTerminalMatchPattern;
                AddEvent(new ExpenseRecipientPosTerminalMatchPatternChanged(ExpenseRecipientId, PosTerminalMatchPattern));
            }
        }

        public void AddPosTerminal(string posTerminalCode)
        {
            PosTerminals.Add(new PosTerminal(posTerminalCode, ExpenseRecipientId));
            this.AddEvent(new PosTerminalAdded(posTerminalCode, ExpenseRecipientId));
        }

        public void AddIban(string ibanCode)
        {
            Ibans.Add(new Iban(ibanCode, ExpenseRecipientId));
            this.AddEvent(new IbanAdded(ibanCode, ExpenseRecipientId));
        }

        public void AddDirectDebit(string directDebitCode)
        {
            DirectDebits.Add(new DirectDebit(directDebitCode, ExpenseRecipientId));
            this.AddEvent(new DirectDebitAdded(directDebitCode, ExpenseRecipientId));
        }

        public void AddOpenBankingMerchant(string merchantCode)
        {
            OpenBankingMerchants.Add(new OpenBankingMerchant(merchantCode, ExpenseRecipientId));
            this.AddEvent(new OpenBankingMerchantAdded(merchantCode, ExpenseRecipientId));
        }


        public override Guid GetIdentityValue() => ExpenseRecipientId;
    }
}
