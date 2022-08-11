using ApartmentsBilling.Cache.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace ApartmentsBilling.Cache.Concrete
{
    public class InMemoryCache : IInMemoryService
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public void Add(string key, object data, int duration)
        {
            _memoryCache.Set(key, data, TimeSpan.FromMinutes(duration));
        }

        public void Add(string key, object data)
        {
            _memoryCache.Set(key, data);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }
    }
}
