using BlazorCalc.Logger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BlazorCalc.Server
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
                .ConfigureLogging((hostBuilderContext, logging) =>
                {
                    logging.AddLogFileFileLogger(options =>
                    {
                        hostBuilderContext.Configuration
                            .GetSection("Logging")
                            .GetSection("LogFile")
                            .GetSection("Options").Bind(options);
                    });
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}