using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.Banking.Domain.CashWithdrawalAggregate;

namespace Pet.Application.Commands.Banking
{
    public class AddCashWithdrawal
    {
        public class Command : NBB.Application.DataContracts.Command, IHaveTransactionDate
        {
            public string CashTerminal { get; }
            public decimal Value { get; }
            public DateTime WithdrawalDate { get; }

            DateTime IHaveTransactionDate.TransactionDate => WithdrawalDate;

            public Command(string cashTerminal, decimal value, DateTime withdrawalDate, CommandMetadata metadata = null)
                : base(metadata)
            {
                CashTerminal = cashTerminal;
                Value = value;
                WithdrawalDate = withdrawalDate;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ICashWithdrawalRepository _repository;

            public Handler(ICashWithdrawalRepository repository)
            {
                _repository = repository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var cashWithdrawal = new CashWithdrawal(request.CashTerminal, request.Value, request.WithdrawalDate);
                await _repository.AddAsync(cashWithdrawal);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
