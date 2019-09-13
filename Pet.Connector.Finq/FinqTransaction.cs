using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Pet.Connector.Finq
{
    public class FinqTransaction
    {
        [JsonProperty("account_id")]
        public Guid AccountId { get; set; }

        public Data Data { get; set; }
        public Guid Id { get; set; }

    }

    public class Data
    {
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        [JsonProperty("booking_date_time")]
        public DateTime BookingDateTime { get; set; }
        [JsonProperty("credit_debit_indicator")]
        public string CreditDebitIndicator { get; set; }
        public string Currency { get; set; }
        public Details Details { get; set; }
        public decimal ExchangeRate { get; set; }
        public string Info { get; set; }
        public string Status { get; set; }
    }

    public class Details
    {
        public List<string> Attach { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Merchant { get; set; }
        public List<string> Tags { get; set; }
    }
}
