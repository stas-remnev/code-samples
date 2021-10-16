using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using LinqToDB.Common;
using Newtonsoft.Json;

namespace Nsd.Web.Api.CorpDb.Controllers.Proxies.Bpm.Model
{
    public class SbpRegistrationAccountInfoModel
    {
        [JsonPropertyName("cmpcodeid")]
        public int Cmpcodeid { get; set; }

        [JsonPropertyName("legalId")]
        public string LegalId { get; set; }

        [JsonPropertyName("inn")]
        public string Inn { get; set; }

        [JsonPropertyName("account")]
        public string Account { get; set; }

        [JsonPropertyName("docid")]
        public int Docid { get; set; }

        [JsonPropertyName("bankaccid")]
        public int AccId { get; set; }
    }
}
