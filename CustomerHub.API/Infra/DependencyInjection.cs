
using CustomerHub.DataAccess;

    namespace CustomerHub.API.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructre(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<CustomerDbContext, CustomerDbContext>();
            services.AddScoped<CustomerService, CustomerService>();
           
              
           return services;
        }
    }
}
