using Classifieds.App.Services.CustomRepositories;
using Classifieds.App.Services.ICustomRepositories;
using Classifieds.App.Services.IRepositories;
using Classifieds.App.Services.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Classifieds.App.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryCollection(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IAdvertisementRepository, AdvertisementRepository>();
            services.AddSingleton<IAttributeRepository, AttributeRepository>();
            services.AddSingleton<IImageRepository, ImageRepository>();
            services.AddSingleton<ILocationRepository, LocationRepository>();
            services.AddSingleton<IAttributeDetailRepository, AttributeDetailRepository>();
            services.AddSingleton<IAdDetailRepository, AdDetailRepository>();
            services.AddSingleton<IAdTileRepository, AdTileRepository>();
            services.AddSingleton<IAdminRepository, AdminRepository>();
            services.AddSingleton<IAdTypeRepository, AdTypeRepository>();
            services.AddSingleton<IReportRepository, ReportRepository>();
            services.AddSingleton<IInboxRepository, InboxRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();
            services.AddSingleton<IOfferRepository, OfferRepository>();
            return services;
        }
    }
}