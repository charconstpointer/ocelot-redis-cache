using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Ocelot.Cache;
using Ocelot.Redis.Interfaces;
using Ocelot.Redis.Models;

namespace Ocelot.Redis.Serialization
{
    public class CachedResponseSerializer : ICachedResponseSerializer
    {
        public IEnumerable<byte> Serialize(CachedResponse response)
        {
            var ms = new MemoryStream();
            var bs = new BinaryFormatter();
            var serializableCachedResponse = new SerializableCachedResponse(response);
            bs.Serialize(ms, serializableCachedResponse);
            return ms.ToArray();
        }

        public CachedResponse Deserialize(byte[] response)
        {
            var ms = new MemoryStream(response);
            var bs = new BinaryFormatter();
            var deserializedCachedValue = bs.Deserialize(ms);
            var cachedValue = (SerializableCachedResponse) deserializedCachedValue;
            var value = new CachedResponse(cachedValue.StatusCode, cachedValue.Headers, cachedValue.Body,
                cachedValue.ContentHeaders, cachedValue.ReasonPhrase);
            return value;
        }
    }
}