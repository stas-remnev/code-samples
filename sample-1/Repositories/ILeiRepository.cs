using Nsd.Repository.Base;
using Nsd.Repository.Ef.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    public interface ILeiRepository : IRepository<LeiJobLaunchInfo>
    {
        bool FilesCreatedTodayExists();
        void SaveLaunchDate(string jobName);
    }
}
