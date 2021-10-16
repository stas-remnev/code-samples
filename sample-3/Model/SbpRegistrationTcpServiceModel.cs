using System;
using System.ComponentModel;
using LinqToDB.Common;

namespace Nsd.Service.CorpDb.Services.Bpm.Model
{
    public class SbpRegistrationTcpServiceModel
    {
        public int Cmpcodeid { get; set; }

        public string LegalId { get; set; }

        public string Inn { get; set; }

        public string BrandName { get; set; }

        public string Mcc { get; set; }

        public string CountryCode { get; set; }

        public string CountrySubDivisionCode { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string Address { get; set; }

        public string ContactPhoneNumber { get; set; }

        public int Docid { get; set; }
    }
}
