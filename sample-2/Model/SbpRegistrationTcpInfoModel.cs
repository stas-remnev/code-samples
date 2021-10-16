using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using LinqToDB.Common;
using Newtonsoft.Json;

namespace Nsd.Web.Api.CorpDb.Controllers.Proxies.Bpm.Model
{
    public class SbpRegistrationTcpInfoModel
    {
        [JsonPropertyName("cmpcodeid")]
        public int Cmpcodeid { get; set; }

        [JsonPropertyName("legalid")]
        public string LegalId { get; set; }

        [JsonPropertyName("inn")]
        public string Inn { get; set; }

        [JsonPropertyName("brandName")]
        public string BrandName { get; set; }

        [JsonPropertyName("mcc")]
        public string Mcc { get; set; }

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

        [JsonPropertyName("contactPhoneNumber")]
        public string ContactPhoneNumber { get; set; }

        [JsonPropertyName("docid")]
        public int Docid { get; set; }
    }
}
