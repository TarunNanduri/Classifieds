using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;

namespace Classifieds.App.Services.Repositories
{
    public class AttributeRepository : Repository<Attribute>, IAttributeRepository
    {
        public AttributeRepository(ClassifiedsContext context) : base(context)
        {
        }
    }
}