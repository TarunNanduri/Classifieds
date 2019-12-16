using System;
using System.Linq;
using System.Threading.Tasks;
using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Classifieds.App.Services.Repositories
{
    public class InboxRepository : Repository<Inbox>, IInboxRepository
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly ClassifiedsContext _context;

        public InboxRepository(ClassifiedsContext context, IAdvertisementRepository advertisementRepository) :
            base(context)
        {
            _context = context;
            _advertisementRepository = advertisementRepository;
        }

        public async Task<Inbox> GetInboxByUserId(int id)
        {
            var inboxList = _context.InBox.ToList();
            var newInbox = new Inbox();
            foreach (var inbox in inboxList)
                if (inbox.UserId == id)
                    newInbox = inbox;
            var advertisements = _advertisementRepository.GetAdsByUser(id).ToList();
            foreach (var advertisement in advertisements)
                if (!advertisement.Expired && !advertisement.Deleted)
                {
                    var advertisementDate = Convert.ToDateTime(advertisement.PostedOn);
                    if ((DateTime.Now - advertisementDate).TotalDays >= advertisement.ExpiryDays)
                    {
                        newInbox.Expired += 1;
                        advertisement.Expired = true;
                    }

                    _context.Entry(advertisement).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    _context.Entry(newInbox).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

            return newInbox;
        }
    }
}