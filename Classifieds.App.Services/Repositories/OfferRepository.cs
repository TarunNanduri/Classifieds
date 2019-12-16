using System;
using System.Collections.Generic;
using System.Linq;
using Classifieds.App.Api.ViewModels;
using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;

namespace Classifieds.App.Services.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly ICategoryRepository _categoryRepository;

        private readonly ClassifiedsContext _context;
        private readonly IImageRepository _imageRepository;
        private readonly IInboxRepository _inboxRepository;

        public OfferRepository(ClassifiedsContext context, IAdvertisementRepository advertisementRepository,
            IAttributeRepository attributeRepository,
            ICategoryRepository categoryRepository, IImageRepository imageRepository,
            IInboxRepository inboxRepository) : base(context)
        {
            _advertisementRepository = advertisementRepository;
            _imageRepository = imageRepository;
            _attributeRepository = attributeRepository;
            _categoryRepository = categoryRepository;
            _context = context;
            _inboxRepository = inboxRepository;
        }

        public IEnumerable<AdTile> GetOfferedAdTiles(int userId)
        {
            var advertisements = _advertisementRepository.GetAdsByUser(userId).ToList();
            var categories = _categoryRepository.GetAll().ToList();
            var attributes = _attributeRepository.GetAll().ToList();
            var images = _imageRepository.GetAll().ToList();
            var adTiles = new List<AdTile>();
            var adTile = new AdTile();
            var offeredAds = new List<Offer>();
            foreach (var advertisement in advertisements.ToList())
                if (advertisement.OfferCount > 0)
                {
                    var offers = _context.Offer.ToList();
                    var newOffer = from offer in offers
                        let userAd = _advertisementRepository.Get(offer.AdvertisementId)
                        where userAd.UserId == userId
                        select offer;
                    offeredAds.AddRange(newOffer);
                }
                else
                {
                    advertisements.Remove(advertisement);
                }

            var totalOfferedAdsCount = offeredAds.Count;
            for (var index = 0; index < totalOfferedAdsCount; index++)
            {
                if (offeredAds[index].AdvertisementId == advertisements[index].Id)
                {
                    adTile.Price = "Offered Amount : ₹ " + offeredAds[index].Price;
                    adTile.TimeStamp = offeredAds[index].TimeStamp;
                    adTile.AdvertisementId = advertisements[index].Id;
                    adTile.OfferedBy = offeredAds[index].UserId;
                }

                foreach (var category in categories)
                    if (category.Id == advertisements[index].CategoryId)
                    {
                        adTile.Category = category.Name;
                        adTile.IconId = category.Icon;
                        break;
                    }

                foreach (var attribute in attributes)
                    switch (attribute.Name)
                    {
                        case "Title" when attribute.AdvertisementId == adTile.AdvertisementId:
                            adTile.Title = attribute.Value;
                            break;
                    }

                foreach (var image in images)
                    if (image.AdvertisementId == adTile.AdvertisementId)
                    {
                        adTile.ImageUrl = image.Img;
                        break;
                    }

                adTiles.Add(adTile);
            }

            return adTiles;
        }

        public string PostOffer(int advertisementId, Offer offer)
        {
            var offers = _context.Offer;
            if (Enumerable.Any(offers,
                offered => offered.UserId == offer.UserId && offered.AdvertisementId == advertisementId))
                return "Already offered";
            var advertisement = _advertisementRepository.Get(advertisementId);
            advertisement.OfferCount += 1;
            _advertisementRepository.Update(advertisement, advertisementId);
            var inboxList = _inboxRepository.GetAll().ToList();
            var newInbox = new Inbox();
            foreach (var inbox in inboxList)
            {
                if (inbox.UserId != advertisement.UserId) continue;
                newInbox = inbox;
                break;
            }

            newInbox.Offers += 1;
            _inboxRepository.Update(newInbox, newInbox.Id);
            offer.TimeStamp = DateTime.Now.ToLocalTime();
            _context.Offer.Add(offer);
            return "done";
        }
    }
}