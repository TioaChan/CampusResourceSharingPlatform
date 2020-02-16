using System.Linq;
using CampusResourceSharingPlatform.Model;
using Microsoft.Extensions.Logging;

namespace CampusResourceSharingPlatform.Web.Data
{
	public static class DbSeedInitializer
	{
		public static void DbSeedInitialize(ApplicationDbContext context,ILogger logger)
		{
			context.Database.EnsureCreated();
			if (context.Users.Any())
			{
				return;
			}
			var users =new ApplicationUser[]
			{
				new ApplicationUser
				{
					Id = "00000000-0000-0000-0000-000000000001",
					UserName = "aaa290",
					NormalizedUserName = "AAA290",
					Email = null,
					NormalizedEmail = null,
					EmailConfirmed = false,
					PasswordHash = "AQAAAAEAACcQAAAAEKBkSaQruCs2w4jzpnSr4GSjZmtaPnTNaRI+F0bqS7LXFCEtVsiKU7adEHFsOqnFSQ==",
					SecurityStamp = "YMXWZD5SUJNY5JHUKA6JE4MIXTPXZP65",
					ConcurrencyStamp = "62767c90-51f0-4b48-8aec-78331662f97d",
					PhoneNumber = null,
					PhoneNumberConfirmed = false,
					TwoFactorEnabled = false,
					LockoutEnd = null,
					LockoutEnabled = true,
					AccessFailedCount = 0,
					IdCardNo = null,
					SchoolCardNo = null,
					StudentIdentityConfirmed = false,
					ProfilePhotoUrl = "/avatar/default.jpg",
					NickName = "admin",
					RealName = null
				}
			};
			logger.LogWarning("DATABASE:No data found in Database,start use the seed to initialize the database.");
			foreach (var applicationUser in users)
			{
				context.Users.Add(applicationUser);
			}
			context.SaveChanges();
			logger.LogInformation("DATABASE:database initialized complete.");
		}
	}
}
