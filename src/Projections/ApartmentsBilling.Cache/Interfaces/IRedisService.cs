namespace ApartmentsBilling.Cache.Interfaces
{
    public interface IRedisService
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object data, int duration);
        void Add(string key, object data);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
