using System;
using System.Collections.Generic;
using System.Linq;
using Pet.Connector.Abstractions;

namespace Pet.Application.Commands.Imports
{
    public class ConnectorResolver
    {
        private readonly IEnumerable<IConnector> _connectors;
        public ConnectorResolver(IEnumerable<IConnector> connectors)
        {
            _connectors = connectors;
        }

        public IConnector GetConnectorFor(string code)
        {
            var c = _connectors.FirstOrDefault(x => x.CanHandle(code));
            if (c == null)
            {
                throw new NotSupportedException($"Import type {code} is not supported.");
            }

            return c;

        }
    }
}
