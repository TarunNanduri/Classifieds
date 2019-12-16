using System.Collections.Generic;
using Classifieds.App.Models;

namespace Classifieds.App.Services.IRepositories
{
    public interface IAdvertisementRepository : IRepository<Advertisement>
    {
        IEnumerable<Advertisement> GetAdsByUser(int userId);
    }
}