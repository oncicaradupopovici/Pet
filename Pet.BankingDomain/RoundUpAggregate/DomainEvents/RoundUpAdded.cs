using System;
using MediatR;
using NBB.Domain;

namespace Pet.Banking.Domain.RoundUpAggregate.DomainEvents
{
    public record RoundUpAdded(Guid RoundUpId, string Iban, decimal Value, DateTime PaymentDate) : INotification;
}
