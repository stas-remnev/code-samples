using System;
using System.Collections.Generic;
using Nsd.CorpDb.TaskScheduler.Jobs.Base;
using NSD.CorpdbCore.Scheduler;
using Nsd.Service.CorpDb.Services.Lei;

namespace Nsd.CorpDb.TaskScheduler.Jobs.Lei
{
    [Job("Job отправки данных на проверку «LeiPreCheckJob»", "LEI")]
    [SimpleSchedule("Default", "0 0,30 12-16 ? * * *")]
    public class LeiPreCheckJob : BaseJob
    {
        private readonly IJobProgressReporterService _progressReporter;
        private readonly ILeiPreCheckService _leiPreCheckService;

        public LeiPreCheckJob(IJobProgressReporterService progressReporter,
            ILeiPreCheckService leiExpirationCheckService,
            BaseJobContext context): base(context)
        {
            _progressReporter = progressReporter;
            _leiPreCheckService = leiExpirationCheckService;
        }

        public override void ExecuteJob(Dictionary<string, object> jobParameters)
        {
            try
            {
                _leiPreCheckService.ProcessChanges();

                _leiPreCheckService.SendLeiToPreCheckProcess();

                _leiPreCheckService.LeiPublicDataFixationProcess();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    LogService.Error("Внутреннее исключение: ", ex, LogContext);
                throw;
            }

        }
    }
}
