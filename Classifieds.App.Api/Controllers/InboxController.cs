using System;
using System.Threading.Tasks;
using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Classifieds.App.Api.Controllers
{
    [Route("classifieds/inbox/count")]
    public class InboxController : ControllerBase
    {
        private readonly IInboxRepository _inboxRepository;

        public InboxController(IInboxRepository inboxRepository)
        {
            _inboxRepository = inboxRepository;
        }

        [HttpGet("{id}")]
        public async Task<Inbox> GetInboxCount(int id)
        {
            try
            {
                return await _inboxRepository.GetInboxByUserId(id);
            }
            catch (Exception)
            {
                return new Inbox();
            }
        }
    }
}