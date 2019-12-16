using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;

namespace Classifieds.App.Services.Repositories
{
    public class AttributeDetailRepository : Repository<AttributeDetail>, IAttributeDetailRepository
    {
        public AttributeDetailRepository(ClassifiedsContext context) : base(context)
        {
        }
    }
}