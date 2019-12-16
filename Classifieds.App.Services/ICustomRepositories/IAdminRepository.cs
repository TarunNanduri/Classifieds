using Classifieds.App.Api.ViewModels;
using Classifieds.App.Common.ViewModels;

namespace Classifieds.App.Services.ICustomRepositories
{
    public interface IAdminRepository
    {
        void PostCategory(NewCategory category);

        void EditCategory(int id, NewCategory category);

        void DeleteCategory(int id);

        CategoryDetail GetCategoryDetail(int id);
    }
}