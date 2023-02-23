using ApartmentsBilling.Cache.Interfaces;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Configuration.Cache
{
    public class Cache_Helper : ICache_Helper
    {
        private readonly IRedisService _redisService;

        public Cache_Helper(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public Task AddCache<T>(string key, T Data)
        {
            _redisService.Add(key, Data, 5);
            return Task.CompletedTask;
        }

        public Task<T> GetValue<T>(string key)
        {
            var val = _redisService.Get<T>(key);
            return Task.FromResult(val);
        }
    }
}
