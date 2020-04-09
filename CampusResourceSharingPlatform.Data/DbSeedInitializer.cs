using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CampusResourceSharingPlatform.Data
{
	public static class DbSeedInitializer
	{
		public static async void DbSeedInitialize(ApplicationDbContext context, ILogger logger)
		{
			context.Database.EnsureCreated();

			#region aaa2900-UsersSeed
			if (!context.Users.Any(x => x.Id == "00000000-0000-0000-0000-000000000001"))
			{
				var users = new[] {
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
					RealName = null,
					DeletedMark = false
				}
			};
				logger.LogWarning("DATABASE:No user data found in Database,start use the seed to initialize the database.");
				foreach (var applicationUser in users)
				{
					context.Users.Add(applicationUser);
				}
				context.SaveChanges();
				logger.LogInformation("DATABASE:default user in database has initialized complete.");
			}
			else
			{
				logger.LogInformation("DATABASE:default user in database checked complete.");
			}
			#endregion

			#region administrator-role-seed
			if (!context.Roles.Any(x => x.Id == "00000000-0000-0000-0000-000000000001"))
			{
				var roles = new[]
				{
					new IdentityRole
					{
						Id = "00000000-0000-0000-0000-000000000001",
						Name = "Administrators",
						NormalizedName = "ADMINISTRATORS",
						ConcurrencyStamp = "02b0c0f7-cf8d-4b7b-8446-3ba70decd1ff"
					},
				};
				logger.LogWarning("DATABASE:No role data found in Database,start use the seed to initialize the database.");
				foreach (var role in roles)
				{
					context.Roles.Add(role);
				}
				context.SaveChanges();
				logger.LogInformation("DATABASE:default role in database has initialized complete.");
			}
			else
			{
				logger.LogInformation("DATABASE:default role in database checked complete.");
			}
			#endregion

			#region user-role relationship
			if (!context.UserRoles.Any(x => x.RoleId == "00000000-0000-0000-0000-000000000001" && x.UserId == "00000000-0000-0000-0000-000000000001"))
			{
				var identityUserRoles = new[]
				{
					new IdentityUserRole<string>
					{
						UserId ="00000000-0000-0000-0000-000000000001",
						RoleId = "00000000-0000-0000-0000-000000000001",
					},
				};
				logger.LogWarning("DATABASE:No user-role relationship data found in Database,start use the seed to initialize the database.");
				foreach (var r in identityUserRoles)
				{
					context.UserRoles.Add(r);
				}
				context.SaveChanges();
				logger.LogInformation("DATABASE:default user-role relationship in database has initialized complete.");
			}
			else
			{
				logger.LogInformation("DATABASE:default user-role relationship in database checked complete.");
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
				new License
				{
					Id = "00000000-0000-0000-0000-000000000016",
					LicenseName = "Log4Net.AspNetCore",
					LicenseType = "Apache Software License 2.0",
					RepoUrl = "https://github.com/huorswords/Microsoft.Extensions.Logging.Log4Net.AspNetCore",
				},
				new License
				{
					Id = "00000000-0000-0000-0000-000000000017",
					LicenseName = "Pomelo.EntityFrameworkCore.MySql",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql",
				}
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

			#region missiontype
			if (!context.MissionTypes.Any())
			{
				var types = new[]
				{
					new MissionType
					{
						Id = "00000000-0000-0000-0000-000000000001",
						TypeName = "Express"
					},
					new MissionType
					{
						Id = "00000000-0000-0000-0000-000000000002",
						TypeName = "Purchase"
					},
					new MissionType
					{
						Id = "00000000-0000-0000-0000-000000000003",
						TypeName = "FleaMarket"
					},
					new MissionType
					{
						Id = "00000000-0000-0000-0000-000000000004",
						TypeName = "Hire"
					},
				};
				logger.LogWarning("DATABASE:No MissionTypes data found in Database,start use the seed to initialize the database.");
				await context.MissionTypes.AddRangeAsync(types);
				context.SaveChanges();
				logger.LogInformation("DATABASE:default MissionTypes data has initialized complete.");
			}
			else
			{
				logger.LogInformation("DATABASE:default MissionTypes in database checked complete.");
			}
			#endregion

			#region ExpressList

			if (!context.ExpressCompanyList.Any())
			{
				var lists = new[]
				{
					new ExpressCompanyList
					{
						Id = "00000000-0000-0000-0000-000000000001",
						CompanyName = "圆通速递",
					},
					new ExpressCompanyList
					{
						Id = "00000000-0000-0000-0000-000000000002",
						CompanyName = "中通速递",
					},
					new ExpressCompanyList
					{
						Id = "00000000-0000-0000-0000-000000000003",
						CompanyName = "顺丰速运",
					},
					new ExpressCompanyList
					{
						Id = "00000000-0000-0000-0000-000000000004",
						CompanyName = "韵达快递",
					},
					new ExpressCompanyList
					{
						Id = "00000000-0000-0000-0000-000000000005",
						CompanyName = "申通快递",
					},
					new ExpressCompanyList
					{
						Id = "00000000-0000-0000-0000-000000000006",
						CompanyName = "中国邮政EMS",
					},
					new ExpressCompanyList
					{
						Id = "00000000-0000-0000-0000-000000000007",
						CompanyName = "中国邮政国内小包",
					},
					new ExpressCompanyList
					{
						Id = "00000000-0000-0000-0000-000000000008",
						CompanyName = "百世快递",
					},
					new ExpressCompanyList
					{
						Id = "00000000-0000-0000-0000-000000000009",
						CompanyName = "天天快递",
					},
					new ExpressCompanyList
					{
						Id = "00000000-0000-0000-0000-000000000010",
						CompanyName = "京东快递",
					},
				};
				logger.LogWarning("DATABASE:No ExpressCompanyList data found in Database,start use the seed to initialize the database.");
				await context.ExpressCompanyList.AddRangeAsync(lists);
				context.SaveChanges();
				logger.LogInformation("DATABASE:default ExpressCompanyList data has initialized complete.");
			}
			else
			{
				logger.LogInformation("DATABASE:default ExpressCompanyList in database checked complete.");
			}
			#endregion

			logger.LogInformation("DATABASE:database checked complete.");
			logger.LogInformation("==========================================================");
		}
	}
}
