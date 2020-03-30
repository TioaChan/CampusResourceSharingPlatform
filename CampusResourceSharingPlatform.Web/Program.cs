using CampusResourceSharingPlatform.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace CampusResourceSharingPlatform.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			CreateDbIfNotExists(host);
			host.Run();
		}

		private static void CreateDbIfNotExists(IHost host)
		{
			using var scope = host.Services.CreateScope();
			var services = scope.ServiceProvider;
			var logger = services.GetRequiredService<ILogger<Program>>();
			try
			{
				var context = services.GetRequiredService<ApplicationDbContext>();
				DbSeedInitializer.DbSeedInitialize(context, logger);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred creating the DB.");
			}
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
