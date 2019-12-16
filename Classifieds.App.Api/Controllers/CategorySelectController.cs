using System;
using System.Collections.Generic;
using System.Linq;
using Classifieds.App.Api.ViewModels;
using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Category = Classifieds.App.Api.ViewModels.Category;

namespace Classifieds.App.Api.Controllers
{
    [Route("classifieds/select-category")]
    public class CategorySelectController : ControllerBase
    {
        private readonly IAttributeDetailRepository _attributeListRepository;
        private readonly IEnumerable<Category> _category;
        private readonly List<CategoryAttributes> _categoryAttributes = new List<CategoryAttributes>();
        private readonly ICategoryRepository _categoryRepository;
        private CategoryAttributes _attribute;
        private IEnumerable<AttributeDetail> _attributes;
        private IEnumerable<Models.Category> _categories;
        private Category _categoryItem;
        private List<Category> _categoryViews;

        public CategorySelectController(ICategoryRepository categoryRepository,
            IAttributeDetailRepository attributeListRepository)
        {
            _categoryRepository = categoryRepository;
            _attributeListRepository = attributeListRepository;
            _attribute = new CategoryAttributes();
            _categoryItem = new Category();
            _attributes = new List<AttributeDetail>();
            _category = new List<Category>();
        }

        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            _categories = _categoryRepository.GetAll().ToList();
            _categoryViews = _category.ToList();
            foreach (var category in _categories)
            {
                _categoryItem.Icon = category.Icon;
                _categoryItem.Id = category.Id;
                _categoryItem.Name = category.Name;
                _categoryViews.Add(_categoryItem);
                _categoryItem = new Category();
            }

            return _categoryViews;
        }

        [HttpGet("{id}")]
        public IEnumerable<CategoryAttributes> Category([FromRoute] int id)
        {
            try
            {
                _attributes = _attributeListRepository.GetAll().ToList();
                foreach (var attributeDetail in _attributes)
                    if (attributeDetail.CategoryId == id)
                    {
                        _attribute.Id = attributeDetail.Id;
                        _attribute.IsMandatory = attributeDetail.IsMandatory;
                        _attribute.Name = attributeDetail.Name;
                        _attribute.Type = attributeDetail.Type;
                        _categoryAttributes.Add(_attribute);
                        _attribute = new CategoryAttributes();
                    }

                return _categoryAttributes;
            }
            catch (Exception)
            {
                return _categoryAttributes;
            }
        }
    }
}