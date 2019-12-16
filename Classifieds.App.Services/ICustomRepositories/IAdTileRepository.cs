using System.Collections.Generic;
using Classifieds.App.Api.ViewModels;
using Classifieds.App.Services.IRepositories;

namespace Classifieds.App.Services.ICustomRepositories
{
    public interface IAdTileRepository : IRepository<AdTile>
    {
        IEnumerable<AdTile> GetAdTiles(string basis, bool isTrue, int userId = 0);
        IEnumerable<AdTile> GetExpiredAdTiles(int userId);
    }
}