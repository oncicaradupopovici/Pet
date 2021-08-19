using System;
using MediatR;

namespace Pet.Banking.Domain.PosPaymentAggregate.DomainEvents
{
    public record PosPaymentAdded
        (Guid PosPaymentId, string PosTerminalCode, decimal Value, DateTime PaymentDate) : INotification;
}
