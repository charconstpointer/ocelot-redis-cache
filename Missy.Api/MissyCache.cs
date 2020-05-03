using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Extensions.Caching.Distributed;
using Missy.Api.Models;
using Newtonsoft.Json;
using Ocelot.Cache;

namespace Missy.Api
{
    public class MissyCache : IOcelotCache<CachedResponse>
    {
        private readonly IDistributedCache _cache;

        public MissyCache(IDistributedCache cache)
        {
            _cache = cache;
        }


        public void Add(string key, CachedResponse value, TimeSpan ttl, string region)
        {
            var ms = new MemoryStream();
            var bs = new BinaryFormatter();
            var serializableCachedResponse = new SerializableCachedResponse(value);
            bs.Serialize(ms, serializableCachedResponse);
            _cache.SetAsync(key, ms.ToArray()).GetAwaiter().GetResult();
        }

        public CachedResponse Get(string key, string region)
        {
            var bytes = _cache.Get(key);
            if (bytes == null)
            {
                return null;
            }

            var ms = new MemoryStream(bytes);
            var bs = new BinaryFormatter();
            var deserializedCachedValue = bs.Deserialize(ms);
            var cachedValue = (SerializableCachedResponse) deserializedCachedValue;
            var value = new CachedResponse(cachedValue.StatusCode, cachedValue.Headers, cachedValue.Body,
                cachedValue.ContentHeaders, cachedValue.ReasonPhrase);
            return value;
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