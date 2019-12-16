using Classifieds.App.Models;

namespace Classifieds.App.Services.IRepositories
{
    public interface IReportRepository : IRepository<Report>
    {
        Report ReportAdvertisement(Report report);
    }
}