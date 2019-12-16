using System.Collections.Generic;
using System.Linq;
using Classifieds.App.Api.ViewModels;
using Classifieds.App.Services.ICustomRepositories;
using Classifieds.App.Services.IRepositories;
using Classifieds.App.Services.Repositories;

namespace Classifieds.App.Services.CustomRepositories
{
    public class AdTileRepository : Repository<AdTile>, IAdTileRepository
    {
        private readonly IAdTypeRepository _adTypeRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageRepository _imageRepository;

        public AdTileRepository(ICategoryRepository categoryRepository,
            IAdvertisementRepository advertisementRepository, IAttributeRepository attributeRepository,
            IImageRepository imageRepository, IAdTypeRepository adTypeRepository,
            ClassifiedsContext context) : base(context)
        {
            _attributeRepository = attributeRepository;
            _imageRepository = imageRepository;
            _advertisementRepository = advertisementRepository;
            _adTypeRepository = adTypeRepository;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<AdTile> GetAdTiles(string basis, bool isTrue, int userid = 0)
        {
            var adTile = new AdTile();
            var categories = _categoryRepository.GetAll().ToList();
            var adTypes = _adTypeRepository.GetAll().ToList();
            var attributes = _attributeRepository.GetAll().ToList();
            var images = _imageRepository.GetAll().ToList();
            var advertisements = userid != 0
                ? _advertisementRepository.GetAdsByUser(userid).ToList()
                : _advertisementRepository.GetAll().ToList();

            var adTiles = new List<AdTile>();
            var totalAds = advertisements.Count();
            for (var index = 0; index < totalAds; index++)
            {
                if (userid != 0)
                    switch (basis)
                    {
                        case "Deleted" when advertisements[index].Deleted != isTrue:
                        case "Reported" when advertisements[index].Reported != isTrue:
                            continue;
                    }
                else
                    switch (basis)
                    {
                        case "Deleted" when advertisements[index].Deleted != isTrue:
                        case "Reported" when advertisements[index].Reported != isTrue:
                            continue;
                    }

                adTile.AdvertisementId = advertisements[index].Id;

                foreach (var category in categories)
                    if (category.Id == advertisements[index].CategoryId)
                    {
                        adTile.Category = category.Name;
                        adTile.IconId = category.Icon;
                        break;
                    }

                foreach (var adType in adTypes)
                    if (adType.Id == advertisements[index].AdTypeId)
                    {
                        adTile.AdType = adType.Name;
                        break;
                    }

                foreach (var attribute in attributes)
                    switch (attribute.Name)
                    {
                        case "Price" when attribute.AdvertisementId == adTile.AdvertisementId:
                            adTile.Price = attribute.Value;
                            break;
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
                adTile = new AdTile();
            }

            return adTiles;
        }

        public IEnumerable<AdTile> GetExpiredAdTiles(int userId)
        {
            var categories = _categoryRepository.GetAll().ToList();
            var adTypes = _adTypeRepository.GetAll().ToList();
            var attributes = _attributeRepository.GetAll().ToList();
            var images = _imageRepository.GetAll().ToList();
            var advertisements = _advertisementRepository.GetAdsByUser(userId).ToList();
            var adTiles = new List<AdTile>();
            var totalAds = advertisements.Count();
            for (var index = 0; index < totalAds; index++)
                if (advertisements[index].Expired)
                {
                    var adTile = new AdTile
                    {
                        AdvertisementId = advertisements[index].Id
                    };

                    foreach (var category in categories)
                        if (category.Id == advertisements[index].CategoryId)
                        {
                            adTile.Category = category.Name;
                            adTile.IconId = category.Icon;
                            break;
                        }

                    foreach (var adType in adTypes)
                        if (adType.Id == advertisements[index].AdTypeId)
                        {
                            adTile.AdType = adType.Name;
                            break;
                        }

                    foreach (var attribute in attributes)
                        switch (attribute.Name)
                        {
                            case "Price" when attribute.AdvertisementId == adTile.AdvertisementId:
                                adTile.Price = attribute.Value;
                                break;
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
    }
}