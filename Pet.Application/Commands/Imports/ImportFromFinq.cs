using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Pet.Application.Commands.Imports
{
    public class ImportFromFinq
    {
        public class Command : NBB.Application.DataContracts.Command
        {
            public Command()
                : base(null)
            {
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
                var connector = _connectorResolver.GetConnectorFor("finq");
                foreach (var command in connector.GetCommandsFromImportStream(null))
                {
                    await _mediator.Send(command, cancellationToken);
                }
            }
        }
    }
}
