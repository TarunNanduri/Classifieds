using System.Collections.Generic;
using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Classifieds.App.Api.Controllers
{
    [Route("classifieds/adTypes")]
    public class AdTypeController : ControllerBase
    {
        private readonly IAdTypeRepository _adTypeRepository;

        public AdTypeController(IAdTypeRepository adTypeRepository)
        {
            _adTypeRepository = adTypeRepository;
        }

        [HttpGet]
        public IEnumerable<AdType> GetAdTypes()
        {
            return _adTypeRepository.GetAll();
        }
    }
}