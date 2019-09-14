﻿using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Xunit;

namespace Pet.Connector.Finq.Tests
{
    public class FinqConnectorTests
    {
        [Fact]
        public void ImportShoulWork()
        {
            var opts = Options.Create(new FinqOptions
            {
                client_id = "9156515c-f896-461b-be10-da1490e2e372",
                client_app_key =
                    "MDAxNmxvY2F0aW9uIGZpbnF3YXJlCjAwMTdpZGVudGlmaWVyIHBldHJpY3MKMDAxNmNpZCBzY29wZT1zZXNzaW9uCjAwMzdjaWQgY2xpZW50X2lkPTkxNTY1MTVjLWY4OTYtNDYxYi1iZTEwLWRhMTQ5MGUyZTM3MgowMDJmc2lnbmF0dXJlIMXM7U8riuY_ZUx8_ncV4zgxvMlo6YrZIeI4mBul5K7pCg",
                client_secret = "2a8659c4-ff9f-4edc-9067-29d9bc480a87",
                API = "https://api.finqware.com/v1/"
            });
            
            //Arrange
            var connector = new FinqConnector(opts);
            var result = connector.GetCommandsFromImportStream(null);

            //Assert
            result.Should().NotBeEmpty();
        }
    }
}
