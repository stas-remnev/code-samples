using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nsd.Repository.Ef.Repositories.Lei.Models
{
    /// <summary>
    /// Стадия кода LEI для документов типа «Заявление на принятие на обслуживание кода LEI» 
    /// </summary>    
    public enum DocLeiAcceptCodeStatus
    {
        [Description(@"ISSUED")]
        [MapValue("ISSUED")]
        Issued,

        [Description(@"LAPSED")]
        [MapValue("LAPSED")]
        Lapsed,

        [Description(@"MERGED")]
        [MapValue("MERGED")]
        Merged,

        [Description(@"RETIRED")]
        [MapValue("RETIRED")]
        Retired
    }
}
