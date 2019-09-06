using System;
using NBB.Domain;
using Pet.Banking.Domain.BankTransferAggregate.DomainEvents;

namespace Pet.Banking.Domain.BankTransferAggregate
{
    public class BankTransfer : EventedAggregateRoot<Guid>
    {
        public Guid BankTransferId { get; private set;}
        public string Iban { get; private set;}
        public string RecipientName { get; private set;}
        public string Details { get; private set;}
        public decimal Value { get; private set;}
        public DateTime PaymentDate { get; private set;}

        //for ef
        private BankTransfer()
        {

        }

        public BankTransfer(string iban, string recipientName, string details, decimal value, DateTime paymentDate)
        {
            BankTransferId = Guid.NewGuid();
            Iban = iban;
            RecipientName = recipientName;
            Details = details;
            Value = value;
            PaymentDate = paymentDate;

            AddEvent(new BankTransferAdded(BankTransferId, Iban, RecipientName, Details, Value, PaymentDate));

        }

        public override Guid GetIdentityValue() => BankTransferId;
    }
}
