using Classifieds.App.Api.ViewModels;
using Classifieds.App.Services.IRepositories;

namespace Classifieds.App.Services.ICustomRepositories
{
    public interface IAdDetailRepository : IRepository<AdDetail>
    {
        void PostAdvertisementDetail(AdDetail detail);

        AdPage GetAdvertisementDetail(int id, int userId);

        void DeleteAdvertisement(int id);
    }
}