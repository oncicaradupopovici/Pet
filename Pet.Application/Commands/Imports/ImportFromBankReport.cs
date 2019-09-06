using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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

            public Handler(IConnector connector, IMediator mediator)
            {
                _connector = connector;
                _mediator = mediator;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                foreach (var command in _connector.GetCommandsFromBankReport(request.ReportStream))
                {
                    await _mediator.Send(command, cancellationToken);
                }
            }
        }
    }
}
