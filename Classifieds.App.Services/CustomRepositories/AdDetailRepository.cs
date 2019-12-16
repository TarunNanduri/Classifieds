using System;
using System.Collections.Generic;
using System.Linq;
using Classifieds.App.Api.ViewModels;
using Classifieds.App.Models;
using Classifieds.App.Services.AzureRepositories;
using Classifieds.App.Services.ICustomRepositories;
using Classifieds.App.Services.IRepositories;
using Classifieds.App.Services.Repositories;
using Microsoft.Extensions.Configuration;
using Attribute = Classifieds.App.Models.Attribute;

namespace Classifieds.App.Services.CustomRepositories
{
    public class AdDetailRepository : Repository<AdDetail>, IAdDetailRepository
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IAttributeDetailRepository _attributeDetailRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IConfiguration _configuration;
        private readonly ClassifiedsContext _context;
        private readonly IImageRepository _imageRepository;
        private readonly IInboxRepository _inboxRepository;
        private readonly IReportRepository _reportRepository;

        public AdDetailRepository(IAdvertisementRepository advertisementRepository, IImageRepository imageRepository,
            IAttributeDetailRepository attributeDetailRepository, ICategoryRepository categoryRepository,
            IAttributeRepository attributeRepository, IInboxRepository inboxRepository,
            IReportRepository reportRepository,
            ClassifiedsContext context, IConfiguration configuration) : base(context)
        {
            _advertisementRepository = advertisementRepository;
            _reportRepository = reportRepository;
            _inboxRepository = inboxRepository;
            _imageRepository = imageRepository;
            _attributeDetailRepository = attributeDetailRepository;
            _attributeRepository = attributeRepository;
            _categoryRepository = categoryRepository;
            _configuration = configuration;
            _context = context;
        }

        public AdPage GetAdvertisementDetail(int id, int userId)
        {
            var adPage = new AdPage
            {
                Images = new List<string>(),
                Attributes = new List<Attributes>()
            };
            var attribute = new Attributes();
            var advertisement = _advertisementRepository.Get(id);
            if (advertisement == null) return adPage;
            var reports = _reportRepository.GetAll().ToList();
            adPage.isReported = reports.Any(report => report.AdvertisementId == id && report.UserId == userId);
            adPage.isDeleted = advertisement.Deleted;
            adPage.UserId = advertisement.UserId;
            adPage.ExpiresIn = advertisement.ExpiryDays;
            adPage.OfferCount = advertisement.OfferCount;
            adPage.ViewCount = advertisement.ViewCount;
            adPage.PostedDateTime = advertisement.PostedOn;
            var category = _categoryRepository.Get(advertisement.CategoryId);
            adPage.Category = category.Name;
            adPage.IconId = category.Icon;
            var images = _imageRepository.GetAll().ToList();
            foreach (var image in images)
                if (image.AdvertisementId == id)
                    adPage.Images.Add(image.Img);
            var attributes = _attributeRepository.GetAll().ToList();
            var length = attributes.Count;
            for (var index = 0; index < length; index++)
                if (attributes.ToList()[index].AdvertisementId == id)
                {
                    attribute.Id = attributes[index].Id;
                    attribute.Name = attributes[index].Name;
                    attribute.Value = attributes[index].Value;
                    adPage.Attributes.Add(attribute);
                    attribute = new Attributes();
                }

            return adPage;
        }

        public void DeleteAdvertisement(int id)
        {
            var advertisement = _advertisementRepository.Get(id);
            advertisement.Deleted = true;
            _advertisementRepository.Update(advertisement, advertisement.Id);
            var inboxList = _inboxRepository.GetAll().ToList();
            var newInbox = new Inbox();
            foreach (var inbox in inboxList)
            {
                if (inbox.UserId != advertisement.UserId) continue;
                newInbox = inbox;
                break;
            }

            newInbox.Deleted += 1;
            _inboxRepository.Update(newInbox, newInbox.Id);
        }


        public async void PostAdvertisementDetail(AdDetail detail)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var image = new Image();
                    var newAttribute = new Attribute();
                    var advertisement = new Advertisement();
                    var ui = new UploadImage(_configuration);

                    advertisement.OfferCount = detail.OfferCount;
                    advertisement.ViewCount = detail.Views;
                    advertisement.PostedOn = detail.PostedDateTime;
                    advertisement.AdTypeId = detail.AdTypeId;
                    advertisement.ExpiryDays = detail.ExpiryDays;
                    advertisement.CategoryId = detail.CategoryType;
                    advertisement.UserId = detail.PostedBy;
                    _advertisementRepository.Add(advertisement);
                    foreach (var attribute in detail.Attributes)
                    {
                        var attributeDetail = _attributeDetailRepository.Get(attribute.Id);
                        newAttribute.Value = attribute.Value;
                        newAttribute.Name = attributeDetail.Name;
                        newAttribute.AdvertisementId = advertisement.Id;
                        _attributeRepository.Add(newAttribute);
                        newAttribute = new Attribute();
                    }

                    foreach (var item in detail.Images)
                    {
                        var url = await ui.UploadImages(item);
                        image.Img = url;
                        image.AdvertisementId = advertisement.Id;
                        _imageRepository.Add(image);
                        image = new Image();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}