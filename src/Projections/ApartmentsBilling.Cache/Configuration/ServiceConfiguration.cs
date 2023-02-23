using ApartmentsBilling.Cache.Concrete;
using ApartmentsBilling.Cache.Configuration.Redis;
using ApartmentsBilling.Cache.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;
using StackExchange.Redis;

namespace ApartmentsBilling.Cache.Configuration
{
    public static class ServiceConfiguration
    {
        public static void CacheService(this IServiceCollection services, IConfiguration configuration)
        {
            var _redis = configuration.GetSection("Redis").Get<RedisImplementation>();
            services.AddScoped<IRedisService, RedisCache>();
            services.AddScoped<IInMemoryService, InMemoryCache>();
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.ConfigurationOptions = new ConfigurationOptions()
                {
                    EndPoints =
                    {
                        { _redis.EndPoint, _redis.PortNumber }
                    },
                    Password = _redis.Password,
                    User = _redis.UserName

                };
            });

            services.AddSingleton(opt =>
            {
                return new RedisEndpoint
                {
                    Host = _redis.EndPoint,
                    Port = _redis.PortNumber,
                    Username = _redis.UserName,
                    Password = _redis.Password,
                };
            });
        }
    }
}
