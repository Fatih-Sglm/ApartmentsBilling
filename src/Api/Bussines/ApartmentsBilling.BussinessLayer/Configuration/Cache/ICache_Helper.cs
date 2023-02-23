using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Configuration.Cache
{
    public interface ICache_Helper
    {
        Task AddCache<T>(string key, T Data);
        Task<T> GetValue<T>(string key);
    }
}