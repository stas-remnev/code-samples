using LinqToDB.Mapping;
using Nsd.Repository.Ef.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nsd.Repository.Ef.Repositories.Lei.Models
{
    [AddToDbContext]
    public class LeiPublicNameLevel2
    {
        [Column("company_id")]
        public int CompanyId { get; set; }

        [Column("parent_company_id")]
        public int ParentCompanyId { get; set; }

        [Column("language_code")]
        public string LanguageCode { get; set; }

        [Column("cmp_name")]
        public string CmpName { get; set; }

        [Column("is_official")]
        public string IsOfficial { get; set; }

        [Column("transliterated")]
        public string Transliterated { get; set; }

        [Column("format_version")]
        public string FormatVersion { get; set; }
    }
}
