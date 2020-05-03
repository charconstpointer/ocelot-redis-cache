using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.Cache;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Missy.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(x =>
            {
                x.InstanceName = "FooBar";
                x.Configuration = "localhost";
            });
            services.AddSingleton<IDictionary<string, CachedResponse>>(provider =>
                new Dictionary<string, CachedResponse>());
            services.AddSingleton<IOcelotCache<CachedResponse>, MissyCache>();
            services.AddOcelot();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseOcelot();
            app.UseRouting();

            app.UseAuthorization();
        }
    }
}