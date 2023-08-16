using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SocialMedia.Application.Abstractions.Caching;
using SocialMedia.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Services.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var cachedData = await _distributedCache.GetStringAsync(key);
            return string.IsNullOrEmpty(cachedData) ? default : JsonConvert.DeserializeObject<T>(cachedData);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan duration)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, serializedValue, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = duration
            });
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

    }
}
