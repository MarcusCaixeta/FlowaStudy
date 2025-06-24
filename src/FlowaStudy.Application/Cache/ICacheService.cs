using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowaStudy.Application.Cache
{
    public interface ICacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync(string key);

        Task SetPriceAsync(Guid id, decimal price, TimeSpan? expiry = null);
        Task<decimal?> GetPriceAsync(Guid id);
    }
}
