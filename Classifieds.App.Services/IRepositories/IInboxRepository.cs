using System.Threading.Tasks;
using Classifieds.App.Models;

namespace Classifieds.App.Services.IRepositories
{
    public interface IInboxRepository : IRepository<Inbox>
    {
        Task<Inbox> GetInboxByUserId(int id);
    }
}