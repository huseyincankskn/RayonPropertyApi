using Newtonsoft.Json;

namespace Entities.VMs
{
    public class CurrencyListExtVm
    {
        public partial class CurrencyList
        {
            [JsonProperty("Tarih_Date")]
            public TarihDate TarihDate { get; set; }
        }

        public class TarihDate
        {
            [JsonProperty("Currency")]
            public Currency[] Currency { get; set; }

            [JsonProperty("Tarih")]
            public string Tarih { get; set; }

            [JsonProperty("Date")]
            public string Date { get; set; }

            [JsonProperty("Bulten_No")]
            public string BultenNo { get; set; }
        }

        public class Currency
        {
            [JsonProperty("Unit")]
            public long Unit { get; set; }

            [JsonProperty("Isim")]
            public string Isim { get; set; }

            [JsonProperty("CurrencyName")]
            public string CurrencyName { get; set; }

            [JsonProperty("ForexBuying")]
            public string ForexBuying { get; set; }

            [JsonProperty("ForexSelling")]
            public string ForexSelling { get; set; }

            [JsonProperty("BanknoteBuying")]
            public string BanknoteBuying { get; set; }

            [JsonProperty("BanknoteSelling")]
            public string BanknoteSelling { get; set; }

            [JsonProperty("CrossRateUSD")]
            public string CrossRateUsd { get; set; }

            [JsonProperty("CrossRateOther")]
            public string CrossRateOther { get; set; }

            [JsonProperty("CrossOrder")]
            public long CrossOrder { get; set; }

            [JsonProperty("Kod")]
            public string Kod { get; set; }

            [JsonProperty("CurrencyCode")]
            public string CurrencyCode { get; set; }
        }
    }
}
