using System.Linq;
using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;

namespace Classifieds.App.Services.Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IInboxRepository _inboxRepository;

        public ReportRepository(ClassifiedsContext context, IAdvertisementRepository advertisementRepository,
            IInboxRepository inboxRepository) :
            base(context)
        {
            _advertisementRepository = advertisementRepository;
            _inboxRepository = inboxRepository;
        }


        public Report ReportAdvertisement(Report report)
        {
            var advertisement = _advertisementRepository.Get(report.AdvertisementId);
            if (advertisement.Deleted) return new Report();
            advertisement.Reported = true;
            _advertisementRepository.Update(advertisement, report.AdvertisementId);
            var inboxList = _inboxRepository.GetAll().ToList();
            var newInbox = new Inbox();
            foreach (var inbox in inboxList)
            {
                if (inbox.UserId != advertisement.UserId) continue;
                newInbox = inbox;
                break;
            }

            newInbox.Reports += 1;
            _inboxRepository.Update(newInbox, newInbox.Id);
            var newReport = new Report
            {
                UserId = report.UserId,
                AdvertisementId = report.AdvertisementId,
                Description = report.Description,
                IsVerified = report.IsVerified
            };
            return newReport;
        }
    }
}