using ApartmentsBilling.DataAccesLayer.Contexts;
using ApartmentsBilling.DataAccesLayer.Features.Abstract;
using ApartmentsBilling.DataAccesLayer.Features.Concrete.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentsBilling.DataAccesLayer
{
    public static class ServiceRegistration
    {
        public static void DataAccesServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApartmentDbContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("MSS"));
                x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
