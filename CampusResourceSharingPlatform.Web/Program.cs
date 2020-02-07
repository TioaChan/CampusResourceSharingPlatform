using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CampusResourceSharingPlatform.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)

			#region Log4Net
			.ConfigureLogging((context, loggingBuilder) =>
			{
				loggingBuilder.AddFilter("System", LogLevel.Warning);//过滤掉命名空间
				loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
				loggingBuilder.AddLog4Net();//使用log4net
			})//扩展日志
			#endregion

			.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
