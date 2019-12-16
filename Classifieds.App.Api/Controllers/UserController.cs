using System;
using System.Collections.Generic;
using System.Linq;
using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Classifieds.App.Api.Controllers
{
    [Route("classifieds/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetResult()
        {
            return Ok(_userRepository.GetAll());
        }

        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            try
            {
                return _userRepository.Get(id);
            }
            catch (Exception)
            {
                return new User();
            }
        }

        [HttpPut]
        public IActionResult PutUser([FromBody] User user)
        {
            try
            {
                var newUser = _userRepository.Get(user.Id);
                _userRepository.Remove(newUser);
                _userRepository.Add(user);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("userIds")]
        public List<int> GetUserIds()
        {
            var users = _userRepository.GetAll();

            return users.Select(user => user.Id).ToList();
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                _userRepository.Add(user);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromQuery] int id)
        {
            var delUser = _userRepository.Get(id);
            _userRepository.Remove(delUser);
            return Ok();
        }
    }
}