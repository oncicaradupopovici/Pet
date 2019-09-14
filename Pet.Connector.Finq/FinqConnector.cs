using Pet.Connector.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NBB.Application.DataContracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Pet.Application.Commands.OpenBanking;

namespace Pet.Connector.Finq
{
    public class FinqConnector : IConnector
    {
        private readonly IOptions<FinqOptions> _options;

        public FinqConnector(IOptions<FinqOptions> options)
        {
            _options = options;
        }

        public IEnumerable<Command> GetCommandsFromImportStream(Stream stream)
        {
            var response = CallFinqApi(_options.Value.client_id, _options.Value.client_app_key,
                _options.Value.client_secret, _options.Value.API).GetAwaiter().GetResult();

            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var finqTransactions = JsonConvert.DeserializeObject<IEnumerable<FinqTransaction>>(response,
                new JsonSerializerSettings {ContractResolver = contractResolver});

            var result = (finqTransactions ?? throw new InvalidOperationException()).Where(IsPayment).Select(ToCommand);
            return result;

        }

        private static async Task<string> CallFinqApi(string client_id, string client_app_key, string client_secret, string api)
        {
            using (var client = new HttpClient())
            {
                var cmd = new
                {
                    client_id, client_app_key, skill = "ing_ro_aisp_sbx_#2.0"
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "sessions");
                request.Headers.Add("Accept", "application/json");
                request.Content = new ObjectContent<object>(cmd, new JsonMediaTypeFormatter());
                client.BaseAddress = new Uri(api);
                var resp = await client.SendAsync(request);
                var session = await resp.Content.ReadAsAsync<StartSessionResponse>();


                var cmd2 = new
                {
                    skill = "ing_ro_aisp_sbx_#2.0",
                    client_id,
                    step = "sca_implicit",
                    data = new { },
                    nonce = session.nonce
                };

                var request2 = new HttpRequestMessage(HttpMethod.Post, $"sessions/{session.session_id}/steps");
                request2.Headers.Add("Accept", "application/json");
                request2.Content = new ObjectContent<object>(cmd2, new JsonMediaTypeFormatter());
                var resp2 = await client.SendAsync(request2);
                var tempToken = await resp2.Content.ReadAsAsync<GetTempTokenResponse>();


                var cmd3 = new
                {
                    client_id = "9156515c-f896-461b-be10-da1490e2e372",
                    client_secret = "2a8659c4-ff9f-4edc-9067-29d9bc480a87",
                    temp_token = tempToken.temp_token
                };

                var request3 = new HttpRequestMessage(HttpMethod.Post, "tokens");
                request3.Headers.Add("Accept", "application/json");
                request3.Content = new ObjectContent<object>(cmd3, new JsonMediaTypeFormatter());
                var resp3 = await client.SendAsync(request3);
                var accessToken = await resp3.Content.ReadAsAsync<ExchangeTokenModel>();

                var query = new
                {
                    client_id,
                    client_secret,
                    accessToken.access_token,
                    accessToken.credentials_id,
                    filter = new { },
                };

                var request4 = new HttpRequestMessage(HttpMethod.Post, "transactions/get");
                request4.Headers.Add("Accept", "application/json");
                request4.Content = new ObjectContent<object>(query, new JsonMediaTypeFormatter());
                var resp4 = await client.SendAsync(request4);
                if (resp4.IsSuccessStatusCode)
                    return await resp4.Content.ReadAsStringAsync();
                return null;
            }
        }

        public bool CanHandle(string code)
        {
            return code.ToLower() == "finq";
        }

        private Command ToCommand(FinqTransaction t)
        {
            return new AddOpenBankingPayment.Command(t.Id, t.Data.Amount, t.Data.BookingDateTime, t.Data.Currency,
                t.Data.ExchangeRate, t.Data.Details.Merchant, t.Data.Details.Category, t.Data.Details.Location);
        }

        private bool IsPayment(FinqTransaction t)
        {
            return t.Data.CreditDebitIndicator == "debit";
        }
        
        public class StartSessionResponse
        {
            public string session_id { set; get; }
            public string nonce { set; get; }
        }

        public class GetTempTokenResponse
        {
            public string temp_token { set; get; }
        }
        
        public class ExchangeTokenModel
        {
            public string access_token { set; get; }
            public string credentials_id { set; get; }
        }
    }
}