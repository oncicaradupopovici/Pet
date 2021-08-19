using System;
using MediatR;

namespace Pet.Banking.Domain.CashWithdrawalAggregate.DomainEvents
{
    public record CashWithdrawalAdded(Guid CashWithdrawalId, string CashTerminal, decimal Value,
        DateTime WithdrawalDate) : INotification;
}
