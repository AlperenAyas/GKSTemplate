using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Abstrations.Services
{
    public interface ICacheService
    {
        string CreateCacheKey<TKey>(object keyObject);
        Task<T> GetCacheValueAsync<T>(string key);
        Task<bool> SetCacheValueAsync<TObject>(string key, object value);
        Task<bool> DeleteCacheValueAsync(string key);
        Task<bool> DeleteCacheValueLikeAsync(string keyPattern);
    }
}
