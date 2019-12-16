using System;
using System.Linq;
using Classifieds.App.Api.ViewModels;
using Classifieds.App.Common.ViewModels;
using Classifieds.App.Models;
using Classifieds.App.Services.ICustomRepositories;
using Classifieds.App.Services.IRepositories;
using Classifieds.App.Services.Repositories;
using Category = Classifieds.App.Models.Category;

namespace Classifieds.App.Services.CustomRepositories
{
    public class AdminRepository : Repository<NewCategory>, IAdminRepository
    {
        private readonly IAttributeDetailRepository _attributeDetailRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AdminRepository(ClassifiedsContext context, ICategoryRepository categoryRepository,
            IAttributeDetailRepository detailRepository) : base(context)
        {
            _categoryRepository = categoryRepository;
            _attributeDetailRepository = detailRepository;
        }

        public void PostCategory(NewCategory category)
        {
            var newCategory = new Category();
            var detail = new AttributeDetail();
            newCategory.CreatedBy = category.CreatedBy;
            newCategory.Name = category.Name;
            newCategory.Description = category.Description;
            newCategory.Icon = category.Icon;
            newCategory.CreatedOn = DateTime.Now;
            _categoryRepository.Add(newCategory);
            foreach (var attribute in category.Attributes)
            {
                detail.CategoryId = newCategory.Id;
                detail.IsMandatory = attribute.IsMandatory;
                detail.Name = attribute.Name;
                detail.Type = attribute.Type;
                _attributeDetailRepository.Add(detail);
                detail = new AttributeDetail();
            }
        }

        public void EditCategory(int id, NewCategory category)
        {
            var detail = _attributeDetailRepository.GetAll().ToList();
            var dbCategory = _categoryRepository.Get(id);

            var newCategory = new Category
            {
                Id = dbCategory.Id,
                Description = category.Description,
                Icon = category.Icon,
                CreatedBy = dbCategory.CreatedBy,
                ModifiedBy = category.CreatedBy,
                Name = category.Name,
                CreatedOn = DateTime.Now
            };
            _categoryRepository.Update(newCategory, newCategory.Id);

            foreach (var attribute in detail.Where(attribute => attribute.CategoryId == id))
                _attributeDetailRepository.Remove(attribute);
            foreach (var attribute in category.Attributes.Select(newAttribute => new AttributeDetail
            {
                CategoryId = newCategory.Id,
                Name = newAttribute.Name,
                Type = newAttribute.Type,
                IsMandatory = newAttribute.IsMandatory
            }))
            {
                _attributeDetailRepository.Add(attribute);
            }
        }

        public void DeleteCategory(int id)
        {
            var detail = _attributeDetailRepository.GetAll().ToList();
            var category = _categoryRepository.Get(id);

            foreach (var attribute in detail.Where(attribute => attribute.CategoryId == id))
                _attributeDetailRepository.Remove(attribute);
            _categoryRepository.Remove(category);
        }


        public CategoryDetail GetCategoryDetail(int id)
        {
            var category = _categoryRepository.Get(id);
            var detail = new CategoryDetail
            {
                Description = category.Description,
                CreatedOn = category.CreatedOn
            };
            return detail;
        }
    }
}