using System;
using MediatR;

namespace Pet.Banking.Domain.ExchangeAggregate.DomainEvents
{
    public record ExchangeAdded(Guid ExchangeId, string Iban, string ExchangeValue, string ExchangeRate, string Details,
        decimal Value, DateTime PaymentDate) : INotification;
}
