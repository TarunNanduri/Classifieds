using System;
using System.Linq;
using Classifieds.App.Api.ViewModels;
using Classifieds.App.Common.ViewModels;
using Classifieds.App.Services.ICustomRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Classifieds.App.Api.Controllers
{
    [Route("classifieds/createCategory")]
    public class CategoryEditController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;


        public CategoryEditController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet("{id}")]
        public CategoryDetail GetCategoryDetail(int id)
        {
            try
            {
                return _adminRepository.GetCategoryDetail(id);
            }

            catch (Exception)
            {
                return new CategoryDetail();
            }
        }

        [HttpPost]
        public IActionResult PostCategory([FromBody] NewCategory category)
        {
            try
            {
                _adminRepository.PostCategory(category);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult EditCategory(int id, [FromBody] NewCategory category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Any())
                        .Select(x => new {x.Key, x.Value.Errors});
                    return BadRequest(errors);
                }

                _adminRepository.EditCategory(id, category);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _adminRepository.DeleteCategory(id);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok();
        }
    }
}