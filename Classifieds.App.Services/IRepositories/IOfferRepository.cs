using System.Collections.Generic;
using Classifieds.App.Api.ViewModels;
using Classifieds.App.Models;

namespace Classifieds.App.Services.IRepositories
{
    public interface IOfferRepository : IRepository<Offer>
    {
        IEnumerable<AdTile> GetOfferedAdTiles(int userId);
        string PostOffer(int advertisementId, Offer offer);
    }
}