using System;
using MediatR;

namespace Pet.Banking.Domain.BankTransferAggregate.DomainEvents
{
    public record BankTransferAdded(Guid BankTransferId, string Iban, string RecipientName, string Details,
        decimal Value, DateTime PaymentDate) : INotification;
}
