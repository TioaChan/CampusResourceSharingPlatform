using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using CampusResourceSharingPlatform.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;

namespace CampusResourceSharingPlatform.Web
{
	public class Startup
	{
		private readonly IWebHostEnvironment _env;
		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			_env = env;
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDatabaseDeveloperPageExceptionFilter();

			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
			services.AddScoped<ILicensesDateService<License>, LicenseDateService>();
			services.AddScoped<IExpressCompanyListService<ExpressCompanyList>, ExpressCompanyListService>();
			services.AddScoped<IMissionService<Express>, TakeExpressService>();
			services.AddScoped<IMissionService<Purchase>, PurchaseService>();
			services.AddScoped<IMissionService<SecondHand>, FleaMarketService>();
			services.AddScoped<IMissionService<Hire>, HireService>();

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),new MySqlServerVersion(new Version(8, 4, 0)))
			);

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
					options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders()
				.AddDefaultUI();

			//set data safety token time limit for 3 hours.
			services.Configure<DataProtectionTokenProviderOptions>(o =>
				o.TokenLifespan = TimeSpan.FromHours(3));

			services.Configure<EmailSettings>(Configuration.GetSection("EmailConfiguration"));
			services.AddSingleton<IEmailSender, EmailSender>();

			services.AddRazorPages();

			if (_env.IsDevelopment())
			{
				services.Configure<IdentityOptions>(options =>
				{
					options.Password.RequireDigit = false;
					options.Password.RequireLowercase = false;
					options.Password.RequireUppercase = false;
					options.Password.RequireNonAlphanumeric = false;
				});
			}
			else if (_env.IsProduction())
			{
				services.Configure<IdentityOptions>(options =>
				{
					options.Password.RequireDigit = true;
					options.Password.RequireLowercase = true;
					options.Password.RequireUppercase = true;
					options.Password.RequireNonAlphanumeric = true;
				});
			}
			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;
				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = true;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();	
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
