using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace AirDoc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHostBuilderExtensions.UseStartup<Startup>(new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration())
                .Build();

            host.Run();
        }
    }
}
