using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;

namespace Classifieds.App.Services.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ClassifiedsContext context) : base(context)
        {
        }
    }
}