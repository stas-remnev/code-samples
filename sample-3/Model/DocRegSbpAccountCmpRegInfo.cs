using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Nsd.Service.CorpDb.Services.Bpm.Model
{
    public class DocRegSbpAccountCmpRegInfo
    {
        [JsonProperty("cmpcodeid")]
        public int Cmpcodeid { get; set; }

        [JsonProperty("legalId")]
        public string LegalId { get; set; }

        [JsonProperty("inn")]
        public string Inn { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("docid")]
        public int Docid { get; set; }

        [JsonProperty("bankaccid")]
        public int AccId { get; set; }
    }
}
