using Inventory.DataAccess.Services;

namespace InventoryWeb
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            return services;
        }
    }
}
