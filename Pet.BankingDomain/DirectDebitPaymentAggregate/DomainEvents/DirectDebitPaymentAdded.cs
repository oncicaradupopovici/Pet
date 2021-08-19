using System;
using MediatR;

namespace Pet.Banking.Domain.DirectDebitPaymentAggregate.DomainEvents
{
    public record DirectDebitPaymentAdded(Guid DirectDebitPaymentId, string DirectDebitCode, decimal Value, string Details,
        DateTime PaymentDate) : INotification;
}
