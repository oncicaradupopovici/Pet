using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NBB.Application.DataContracts;


namespace Pet.Connector.Abstractions
{
    public interface IConnector
    {
        IEnumerable<Command> GetCommandsFromImportStream(Stream stream);
        bool CanHandle(string code);
    }
}
