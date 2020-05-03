using System;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using Ocelot.Cache;
using Ocelot.Redis.Interfaces;

namespace Ocelot.Redis.Cache
{
    public class RedisCache : IOcelotCache<CachedResponse>
    {
        private readonly IDistributedCache _cache;
        private readonly ICachedResponseSerializer _serializer;

        public RedisCache(ICachedResponseSerializer serializer, IDistributedCache cache)
        {
            _serializer = serializer;
            _cache = cache;
        }

        public void Add(string key, CachedResponse value, TimeSpan ttl, string region)
        {
            var serializedValue = _serializer.Serialize(value);
            _cache.SetAsync(key, serializedValue.ToArray()).GetAwaiter().GetResult();
        }

        public CachedResponse Get(string key, string region)
        {
            var value = _cache.GetAsync(key).GetAwaiter().GetResult();
            if (value is null) return null;
            var deserialized = _serializer.Deserialize(value);
            return deserialized;
        }

        public void ClearRegion(string region)
        {
            throw new NotImplementedException();
        }

        public void AddAndDelete(string key, CachedResponse value, TimeSpan ttl, string region)
        {
            throw new NotImplementedException();
        }
    }
}