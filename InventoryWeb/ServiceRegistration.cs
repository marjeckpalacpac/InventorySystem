using Inventory.DataAccess.Services;

namespace InventoryWeb
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ILookupListingService, LookupListingService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
