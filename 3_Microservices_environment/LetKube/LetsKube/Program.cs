using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog;
using System.IO;

namespace LetsKube
{
	public class Program
	{
		public static void Main(string[] args) {
			LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => {
					webBuilder.UseStartup<Startup>();
				});
	}
}
