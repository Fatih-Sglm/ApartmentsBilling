using ApartmentsBilling.Cache.Interfaces;
using ServiceStack.Redis;
using System;

namespace ApartmentsBilling.Cache.Concrete
{
    public class RedisCache : IRedisService
    {
        private readonly RedisEndpoint _redisEndpoint;
        private readonly RedisClient _redisClient;

        public RedisCache(RedisEndpoint redisEndpoint)
        {

            _redisEndpoint = redisEndpoint;
            _redisClient = new RedisClient(_redisEndpoint);
        }
        public T Get<T>(string key)
        {

            return _redisClient.Get<T>(key);
        }

        public object Get(string key)
        {

            return _redisClient.Get(key);

        }

        public void Add(string key, object data, int duration)
        {

            _redisClient.Set(key, data, TimeSpan.FromMinutes(duration));

        }

        public void Add(string key, object data)
        {

            _redisClient.Set(key, data);

        }

        public bool IsAdd(string key)
        {

            return _redisClient.ContainsKey(key);

        }

        public void Remove(string key)
        {

            _redisClient.Remove(key);

        }

        public void RemoveByPattern(string pattern)
        {
            _redisClient.RemoveByPattern(pattern);
        }
    }
}
