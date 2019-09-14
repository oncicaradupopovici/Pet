using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Pet.Connector.Finq.Tests
{
    public class FinqConnectorTests
    {
        [Fact]
        public void ImportShoulWork()
        {
            //Arrange
            var connector = new FinqConnector();


            var result = connector.GetCommandsFromImportStream(null);

            //Assert
            result.Should().NotBeEmpty();
        }
    }
}
