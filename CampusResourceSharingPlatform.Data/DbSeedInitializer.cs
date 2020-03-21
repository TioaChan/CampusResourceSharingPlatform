using System.Collections.Generic;
using System.Linq;
using CampusResourceSharingPlatform.Model;
using Microsoft.Extensions.Logging;

namespace CampusResourceSharingPlatform.Data
{
	public static class DbSeedInitializer
	{
		public static void DbSeedInitialize(ApplicationDbContext context,ILogger logger)
		{
			context.Database.EnsureCreated();

			#region UsersSeed
			if (!context.Users.Any()) { 
				var users = new[]
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
				logger.LogWarning("DATABASE:No user data found in Database,start use the seed to initialize the database.");
				foreach (var applicationUser in users)
				{
					context.Users.Add(applicationUser);
				}
				context.SaveChanges();
				logger.LogInformation("DATABASE:database initialized complete.");
			}
			#endregion

			#region ThirdLicensesSeed
			if (!context.ThirdLicenses.Any())
			{
				var licenses = new[]
			{
				new License
				{
					Id = "00000000-0000-0000-0000-000000000001",
					LicenseName = "dotnet",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/microsoft/dotnet",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000002",
					LicenseName = "EntityFrameworkCore",
					LicenseType = "Apache Software License 2.0",
					RepoUrl = "https://github.com/aspnet/EntityFrameworkCore",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000003",
					LicenseName = "jQuery",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/jquery/jquery",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000004",
					LicenseName = "Bootstrap",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/twbs/bootstrap",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000005",
					LicenseName = "bootstrap-icons",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/twbs/icons",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000006",
					LicenseName = "jquery-validation",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/jquery-validation/jquery-validation",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000008",
					LicenseName = "jquery-validation-unobtrusive",
					LicenseType = "Apache Software License 2.0",
					RepoUrl = "https://github.com/aspnet/jquery-validation-unobtrusive",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000009",
					LicenseName = "Popper.js",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/popperjs/popper.js",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000010",
					LicenseName = "startbootstrap-agency",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/BlackrockDigital/startbootstrap-agency",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000011",
					LicenseName = "Noto Serif",
					LicenseType = "Apache Software License 2.0",
					RepoUrl = "https://fonts.google.com/specimen/Noto+Serif",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000012",
					LicenseName = "Noto Sans SC",
					LicenseType = "Open Font License",
					RepoUrl = "https://fonts.google.com/specimen/Noto+Sans+SC",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000013",
					LicenseName = "jquery-qrcode",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/jeromeetienne/jquery-qrcode",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000014",
					LicenseName = "Login Form 18 by Colorlib Modify by TioaTyan.",
					LicenseType = "CC BY 3.0",
					RepoUrl = "https://colorlib.com/wp/template/login-form-v18/",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000015",
					LicenseName = "LibMan",
					LicenseType = "Apache Software License 2.0",
					RepoUrl = "https://github.com/aspnet/LibraryManager",
				},
			};
				logger.LogWarning("DATABASE:No Licenses data found in Database,start use the seed to initialize the database.");
				foreach (var license in licenses)
				{
					context.ThirdLicenses.Add(license);
				}
				context.SaveChanges();
				logger.LogInformation("DATABASE:database initialized complete.");
				}
			#endregion

			logger.LogInformation("DATABASE:database checked complete.");
		}
	}
}
