using System.Collections.Generic;
using Classifieds.App.Api.ViewModels;
using Classifieds.App.Services.ICustomRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Classifieds.App.Api.Controllers
{
    [Route("classifieds/adTiles")]
    public class AdTileController : ControllerBase
    {
        private readonly IAdTileRepository _adTileRepository;

        public AdTileController(IAdTileRepository adTileRepository)
        {
            _adTileRepository = adTileRepository;
        }

        [HttpGet]
        public string CheckAzure()
        {
            return "Success";
        }

        [HttpGet("deleted/{isDeleted}")]
        public IEnumerable<AdTile> GetAdTiles(bool isDeleted)
        {
            return _adTileRepository.GetAdTiles("Deleted", isDeleted);
        }

        [HttpGet("{userId}/deleted/{isDeleted}")]
        public IEnumerable<AdTile> GetAdTilesByUser(int userId, bool isDeleted)
        {
            return _adTileRepository.GetAdTiles("Deleted", isDeleted, userId);
        }

        [HttpGet("reported/{isReported}")]
        public IEnumerable<AdTile> GetAdTile(bool isReported)
        {
            return _adTileRepository.GetAdTiles("Reported", isReported);
        }

        [HttpGet("{userId}/reported/{isReported}")]
        public IEnumerable<AdTile> GetReportedTilesByUser(int userId, bool isReported)
        {
            return _adTileRepository.GetAdTiles("Reported", isReported, userId);
        }

        [HttpGet("{userId}/expired")]
        public IEnumerable<AdTile> GetExpiredAdTiles(int userId)
        {
            return _adTileRepository.GetExpiredAdTiles(userId);
        }
    }
}