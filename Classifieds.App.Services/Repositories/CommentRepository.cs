using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;

namespace Classifieds.App.Services.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ClassifiedsContext context) : base(context)
        {
        }
    }
}