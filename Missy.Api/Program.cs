using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Missy.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder => builder.AddJsonFile("ocelot.json"))
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}