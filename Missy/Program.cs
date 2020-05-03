using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Missy
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://www.youtube.com/watch?v=uNCGM7FJ8wQ");
            var data = await response.Content.ReadAsStringAsync();
            Console.WriteLine(data);
        }
    }
}