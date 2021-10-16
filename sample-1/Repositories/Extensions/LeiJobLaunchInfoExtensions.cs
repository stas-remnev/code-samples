using Nsd.Repository.Base;
using Nsd.Repository.Ef.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nsd.Repository.Ef.Repositories.Lei.Extensions
{
    public static class LeiJobLaunchInfoExtensions
    {
        public static int GetIterationNum(this IReadOnlyRepository<LeiJobLaunchInfo> repository)
        {
            var iterationNumber = repository.Get().Where(x => (x.JobName == "LeiJob" || x.JobName == "LeiForZNOJob") && x.LaunchDt.Date == DateTime.Now.Date).DefaultIfEmpty().Select(x => x.IterationNum).ToList().Max();
            
            if (iterationNumber != 0)
                iterationNumber = 0;
            else
                ++iterationNumber;
            return iterationNumber;
        }
    }
}
