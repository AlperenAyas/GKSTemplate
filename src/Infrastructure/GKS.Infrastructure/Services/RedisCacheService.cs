using GKS.Application.Abstrations.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connection;
        private readonly IConfiguration _configuration;

        public RedisCacheService(IConnectionMultiplexer connection, IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }

        public async Task<bool> DeleteCacheValueAsync(string key)
        {
            var db = _connection.GetDatabase();
            return await db.KeyDeleteAsync(key);
            
        }

        public async Task<bool> DeleteCacheValueLikeAsync(string keyPattern)
        {

            var server = _connection.GetServer(_configuration.GetSection("Redis")["Host"] + ":" + _configuration.GetSection("Redis")["Port"]);
            
            foreach (var key in server.Keys(pattern:"*"+keyPattern+"*"))
            {
                await _connection.GetDatabase().KeyDeleteAsync(key);
            }
            return true;
        }

        public string CreateCacheKey<TKey>(object keyObject)
        {
            string cacheKey="";
            var data = typeof(TKey);
            cacheKey += data.Name;
            var properties = data.GetProperties();
            foreach (var property in properties)
            {
                cacheKey +="."+ property.Name.ToString();
                cacheKey +="="+ property.GetValue(keyObject)+"/";
            }
            return cacheKey;
        }

        public async Task<T> GetCacheValueAsync<T>(string key)
        {
            var db = _connection.GetDatabase();
            var value = await db.StringGetAsync(key);
            if(value.HasValue)
                return JsonConvert.DeserializeObject<T>(value);
            return default(T);
        }

        public async Task<bool> SetCacheValueAsync<TObject>(string key, object value)
        {
            var db = _connection.GetDatabase();
            return await db.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }
    }
}
