using System.Collections.Generic;
using System.IO;
using NBB.Application.DataContracts;


namespace Pet.Connector.Abstractions
{
    public interface IConnector
    {
        IEnumerable<Command> GetCommandsFromBankReport(Stream stream);
    }
}
