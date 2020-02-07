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

			#region MyRegion
			.ConfigureLogging((context, loggingBuilder) =>
			{
				loggingBuilder.AddFilter("System", LogLevel.Warning);//���˵������ռ�
				loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
				loggingBuilder.AddLog4Net();//ʹ��log4net
			})//��չ��־
			#endregion

			.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
