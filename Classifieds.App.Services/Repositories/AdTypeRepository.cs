using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;

namespace Classifieds.App.Services.Repositories
{
    public class AdTypeRepository : Repository<AdType>, IAdTypeRepository
    {
        public AdTypeRepository(ClassifiedsContext context) : base(context)
        {
        }
    }
}