using System;
using System.Collections.Generic;
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
            public string Bank { get; set; }

            public Command(Stream reportStream, string bank)
                : base(null)
            {
                ReportStream = reportStream;
                Bank = bank;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ConnectorResolver _connectorResolver;
            private readonly IMediator _mediator;

            public Handler(ConnectorResolver connectorResolver, IMediator mediator)
            {
                _connectorResolver = connectorResolver;
                _mediator = mediator;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var connector = _connectorResolver.GetConnectorFor(request.Bank);
                foreach (var command in connector.GetCommandsFromImportStream(request.ReportStream))
                {
                    await _mediator.Send(command, cancellationToken);
                }
            }
        }
    }
}
