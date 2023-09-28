using Hangfire;
using IBCL.Application.Common.Interfaces;

namespace IBCL.Infrastructure.Hangfire
{
    public static class RecurringJobs
    {
        [Obsolete]
        public static void GetMinutelyAssetsReport()
        {
            RecurringJob.AddOrUpdate<IAssetReportService>(nameof(IAssetReportService.GetMinutelyAssetsReport), x =>
                  x.GetMinutelyAssetsReport(), Cron.MinuteInterval(5), TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));//2 dakkada bir

        }
    }
}
