using System;
using Classifieds.App.Api.ViewModels;
using Classifieds.App.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Classifieds.App.Api.Controllers
{
    [Route("classifieds/userDetails")]
    public class UserDetailController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly UserDetail _userDetail;
        private readonly IUserRepository _userRepository;


        public UserDetailController(IUserRepository userRepository, ILocationRepository locationRepository)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _userDetail = new UserDetail();
        }

        [HttpGet("{id}")]
        public UserDetail GetUserDetails(int id)
        {
            try
            {
                var user = _userRepository.Get(id);
                var location = _locationRepository.Get(user.LocationId);
                _userDetail.Location = location.Name;
                _userDetail.ContactNo = user.ContactNo;
                _userDetail.MailId = user.MailId;
                _userDetail.Name = user.Name;
                _userDetail.Photo = user.Photo;
                return _userDetail;
            }
            catch (Exception)
            {
                return new UserDetail();
            }
        }
    }
}