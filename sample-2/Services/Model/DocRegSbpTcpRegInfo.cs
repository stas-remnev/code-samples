using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Nsd.Service.CorpDb.Services.Bpm.Model
{
    public class DocRegSbpTcpRegInfo
    {
        [JsonProperty("cmpcodeid")]
        public int Cmpcodeid { get; set; }

        [JsonProperty("legalId")]
        public string LegalId { get; set; }

        [JsonProperty("inn")]
        public string Inn { get; set; }

        [JsonProperty("brandName")]
        public string BrandName { get; set; }

        [JsonProperty("mcc")]
        public string Mcc { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("countrySubDivisionCode")]
        public string CountrySubDivisionCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("contactPhoneNumber")]
        public string ContactPhoneNumber { get; set; }

        [JsonProperty("docid")]
        public int Docid { get; set; }
    }
}
