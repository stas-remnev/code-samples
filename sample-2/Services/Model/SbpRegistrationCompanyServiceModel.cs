using System;
using System.ComponentModel;
using LinqToDB.Common;

namespace Nsd.Service.CorpDb.Services.Bpm.Model
{
    public class SbpRegistrationCompanyServiceModel
    {
        public string name { get; set; }

        public string type { get; set; }

        public string inn { get; set; }

        public string kpp { get; set; }

        [DefaultValue("")]
        public string Kpp => kpp.IsNullOrWhiteSpace() ? default : kpp;

        public string ogrn { get; set; }

        public string countryCode { get; set; }

        public string countrySubDivisionCode { get; set; }

        public string city { get; set; }

        public string zip { get; set; }

        public string address { get; set; }

        public string personcode { get; set; }

        public int cmpcodeid { get; set; }

        public string psid { get; set; }

        public int docid { get; set; }
    }
}
