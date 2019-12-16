using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;

namespace Classifieds.App.Services.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(ClassifiedsContext context) : base(context)
        {
        }
    }
}