using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Common;
using LinqToDB.Data;
using Nsd.Repository.Ef.Model.Entities;

namespace Nsd.Repository.Ef.Repositories.Lei
{
    class LeiRepository : BaseRepository<LeiJobLaunchInfo>, ILeiRepository
    {
        public LeiRepository(BaseRepositoryContext context) : base(context)
        {
        }

        /// <summary>
        /// Определение, создавались ли уже сегодня файлы
        /// </summary>
        /// <returns></returns>
        public bool FilesCreatedTodayExists()
        {
            return !Query<LeiJobLaunchInfo>(@"select	top 1 1
                                        from	lei_job_launch_info
                                        where	dbo.date_only(launch_dt) = dbo.date_only(getdate())
                                          and   job_name not in ('ASBISINJob')", new { }).ToString().IsNullOrWhiteSpace();
        }

        /// <summary>
        /// Сохранение времени успешного выполнения джоба
        /// </summary>
        /// <param name="jobName"></param>
        public void SaveLaunchDate(string jobName)
        {
            var today = DateTime.Now;

            var iterationNum = Get().Where(x => x.JobName == jobName && x.LaunchDt == today).DefaultIfEmpty().Select(x => x.IterationNum).ToList().Max();

            if (iterationNum != 0)
                ++iterationNum;

            var leiJobLaunchInfo = new LeiJobLaunchInfo
            {
                JobName = jobName,
                LaunchDt = today,
                IterationNum = iterationNum
            };
            Insert(leiJobLaunchInfo);
        }
    }
}
