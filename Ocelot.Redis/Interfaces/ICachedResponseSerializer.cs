using System.Collections.Generic;
using Ocelot.Cache;

namespace Ocelot.Redis.Interfaces
{
    public interface ICachedResponseSerializer
    {
        IEnumerable<byte> Serialize(CachedResponse response);
        CachedResponse Deserialize(byte[] response);
    }
}