using System;

namespace Pet.Application.Commands.Banking
{
    interface IHaveTransactionDate
    {
        DateTime TransactionDate { get; }
    }
}
