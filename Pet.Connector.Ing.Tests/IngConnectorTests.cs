using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Pet.Connector.Ing.Tests
{
    public class IngConnectorTests
    {
        [Fact]
        public void ImportFromRealReport()
        {
            //Arrange
            var connector = new IngConnector();
            var existingFile = new FileInfo("D:\\finante\\ING Bank - Extras de cont mai_2019_RO96INGB5506999901066266_RON.xlsx");
            using (var stream = existingFile.OpenRead())
            {

                //Act
                var commands = connector.GetCommandsFromImportStream(stream).ToList();
                //Assert
                commands.Should().NotBeEmpty();
            }
        }
    }
}
