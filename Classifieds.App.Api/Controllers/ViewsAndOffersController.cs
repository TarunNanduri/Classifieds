using System;
using System.Collections.Generic;
using Classifieds.App.Api.ViewModels;
using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Classifieds.App.Api.Controllers
{
    [Route("classifieds/views_offers")]
    public class ViewsAndOffersController : ControllerBase
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IOfferRepository _offerRepository;

        public ViewsAndOffersController(IAdvertisementRepository advertisementRepository,
            IOfferRepository offerRepository)
        {
            _advertisementRepository = advertisementRepository;
            _offerRepository = offerRepository;
        }

        [HttpGet("offers/{userId}")]
        public IEnumerable<AdTile> GetOffersByUser(int userId)
        {
            return _offerRepository.GetOfferedAdTiles(userId);
        }

        [HttpPut("{advertisementId}")]
        public IActionResult AddView(int advertisementId)
        {
            try
            {
                var advertisement = _advertisementRepository.Get(advertisementId);
                advertisement.ViewCount += 1;
                _advertisementRepository.Update(advertisement, advertisementId);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("{advertisementId}")]
        public IActionResult OfferAnAdvertisement(int advertisementId, [FromBody] Offer offer)
        {
            try
            {
                var result = _offerRepository.PostOffer(advertisementId, offer);
                if (result == "done")
                    return Ok();
                return BadRequest(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}