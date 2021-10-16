using System;
using System.ComponentModel;
using LinqToDB.Common;

namespace Nsd.Service.CorpDb.Services.Bpm.Model
{
    public class SbpRegistrationAccountServiceModel
    {
        public int Cmpcodeid { get; set; }

        public string LegalId { get; set; }

        public string Inn { get; set; }

        public string Account { get; set; }

        public int Docid { get; set; }

        public int AccId { get; set; }
    }
}
