using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pet.Application.Commands.Banking;
using Pet.Connector.Abstractions;

namespace Pet.Application.Commands.Imports
{
    public class ImportFromBankReport
    {
        public class Command : NBB.Application.DataContracts.Command
        {
            public Stream ReportStream { get; set; }

            public Command(Stream reportStream)
                : base(null)
            {
                ReportStream = reportStream;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IConnector _connector;
            private readonly IMediator _mediator;
            private readonly IAsyncEnumerable<ReadModel.Projections.Expense> _expenseQuery;

            public Handler(IConnector connector, IMediator mediator, IAsyncEnumerable<ReadModel.Projections.Expense> expenseQuery)
            {
                _connector = connector;
                _mediator = mediator;
                _expenseQuery = expenseQuery;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var lastTransactionDate = (await _expenseQuery.MaxAsync(x => x.ExpenseDate, cancellationToken)).Date;

                var commands =
                    _connector.GetCommandsFromBankReport(request.ReportStream)
                    .Where(c =>
                    {
                        if (c is IHaveTransactionDate x)
                        {
                            return x.TransactionDate.Date > lastTransactionDate;
                        }
                        return true;
                    })
                    .ToList();

                foreach (var command in commands)
                {
                    await _mediator.Send(command, cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
