using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using LinqToDB.Common;
using Newtonsoft.Json;

namespace Nsd.Web.Api.CorpDb.Controllers.Proxies.Bpm.Model
{
    public class SbpRegistrationCompanyInfoModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("inn")]
        public string Inn { get; set; }

        [JsonPropertyName("kpp")]
        public string Kpp { get; set; }

        [JsonPropertyName("ogrn")]
        public string Ogrn { get; set; }

        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("countrySubDivisionCode")]
        public string CountrySubDivisionCode { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("zip")]
        public string Zip { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("personcode")]
        public string Personcode { get; set; }

        [JsonPropertyName("cmpcodeid")]
        public int Cmpcodeid { get; set; }

        [JsonPropertyName("psid")]
        public string Psid { get; set; }
        
        [JsonPropertyName("docid")]
        public int Docid { get; set; }
    }
}
