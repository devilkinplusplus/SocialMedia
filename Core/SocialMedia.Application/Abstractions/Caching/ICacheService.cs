using SocialMedia.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Abstractions.Caching
{
    public interface ICacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan duration);
        Task<T> GetAsync<T>(string key);
        Task RemoveAsync(string key);
    }
}
