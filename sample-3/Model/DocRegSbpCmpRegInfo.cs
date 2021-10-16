using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Nsd.Service.CorpDb.Services.Bpm.Model
{
    public class DocRegSbpCmpRegInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("inn")]
        public string Inn { get; set; }

        [JsonProperty("kpp")]
        public string Kpp { get; set; }

        [JsonProperty("ogrn")]
        public string Ogrn { get; set; }

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

        [JsonProperty("personcode")]
        public string Personcode { get; set; }

        [JsonProperty("cmpcodeid")]
        public int Cmpcodeid { get; set; }

        [JsonProperty("psid")]
        public string Psid { get; set; }

        [JsonProperty("docid")]
        public int Docid { get; set; }
    }
}
