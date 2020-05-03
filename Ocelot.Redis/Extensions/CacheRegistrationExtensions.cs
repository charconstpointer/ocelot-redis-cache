using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Cache;
using Ocelot.DependencyInjection;
using Ocelot.Redis.Cache;
using Ocelot.Redis.Interfaces;
using Ocelot.Redis.Serialization;

namespace Ocelot.Redis.Extensions
{
    public static class CacheRegistrationExtensions
    {
        public static void AddRedisCache(this IOcelotBuilder builder, string connectionString,
            string instanceName = null)
        {
            builder.Services.AddTransient<ICachedResponseSerializer, CachedResponseSerializer>();
            builder.Services.AddStackExchangeRedisCache(x =>
            {
                x.InstanceName = instanceName ?? DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
                x.Configuration = connectionString;
            });
            builder.Services.AddSingleton<IOcelotCache<CachedResponse>, RedisCache>();
        }
    }
}