using System;
using System.Linq;
using Classifieds.App.Api.ViewModels;
using Classifieds.App.Services.ICustomRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Classifieds.App.Api.Controllers
{
    [Route("classifieds/AdDetails")]
    public class AdDetailController : ControllerBase
    {
        private readonly IAdDetailRepository _adDetailRepository;

        public AdDetailController(IAdDetailRepository adDetailRepository)
        {
            _adDetailRepository = adDetailRepository;
        }

        [HttpGet("{userid}/{id}")]
        public AdPage GetAdvertisementPage(int id, int userid)
        {
            try
            {
                return _adDetailRepository.GetAdvertisementDetail(id, userid);
            }
            catch (Exception)
            {
                return new AdPage();
            }
        }

        [HttpPost]
        public IActionResult PostAdvertisement([FromBody] AdDetail detail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Any())
                        .Select(x => new {x.Key, x.Value.Errors});
                    return BadRequest(errors);
                }

                _adDetailRepository.PostAdvertisementDetail(detail);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdvertisement(int id)
        {
            try
            {
                _adDetailRepository.DeleteAdvertisement(id);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok();
        }
    }
}