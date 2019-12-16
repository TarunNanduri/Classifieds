using System.Collections.Generic;
using System.Linq;
using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;

namespace Classifieds.App.Services.Repositories
{
    public class AdvertisementRepository : Repository<Advertisement>, IAdvertisementRepository
    {
        private readonly ClassifiedsContext _context;

        public AdvertisementRepository(ClassifiedsContext context) : base(context)
        {
            _context = context;
        }

        IEnumerable<Advertisement> IAdvertisementRepository.GetAdsByUser(int userId)
        {
            var userAds = from ad in _context.Advertisement where ad.UserId == userId select ad;
            return userAds;
        }
    }
}