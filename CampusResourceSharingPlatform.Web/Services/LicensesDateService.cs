using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Web.Models;

namespace CampusResourceSharingPlatform.Web.Services
{
	public class LicenseDateService:ILicensesDateService<License>
	{
		private readonly List<License> _licenses;

		public LicenseDateService()
		{
			_licenses = new List<License>
			{
				new License
				{
					Id = 1,
					LicenseName = "dotnet",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/microsoft/dotnet",
				},
				new License
				{
					Id = 2,
					LicenseName = "EntityFrameworkCore",
					LicenseType = "Apache Software License 2.0",
					RepoUrl = "https://github.com/aspnet/EntityFrameworkCore",
				},
				new License
				{
					Id = 3,
					LicenseName = "jQuery",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/jquery/jquery",
				},
				new License
				{
					Id = 4,
					LicenseName = "Bootstrap",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/twbs/bootstrap",
				},
				new License
				{
					Id = 5,
					LicenseName = "bootstrap-icons",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/twbs/icons",
				},
				new License
				{
					Id = 6,
					LicenseName = "jquery-validation",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/jquery-validation/jquery-validation",
				},
				new License
				{
					Id = 7,
					LicenseName = "jquery-validation-unobtrusive",
					LicenseType = "Apache Software License 2.0",
					RepoUrl = "https://github.com/aspnet/jquery-validation-unobtrusive",
				},
				new License
				{
					Id = 8,
					LicenseName = "Popper.js",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/popperjs/popper.js",
				},
				new License
				{
					Id = 9,
					LicenseName = "startbootstrap-agency",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/BlackrockDigital/startbootstrap-agency",
				},
				new License
				{
					Id = 10,
					LicenseName = "Noto Serif",
					LicenseType = "Apache Software License 2.0",
					RepoUrl = "https://fonts.google.com/specimen/Noto+Serif",
				},
				new License
				{
					Id = 11,
					LicenseName = "Noto Sans SC",
					LicenseType = "Open Font License",
					RepoUrl = "https://fonts.google.com/specimen/Noto+Sans+SC",
				},
				new License
				{
					Id = 12,
					LicenseName = "QRCode.js",
					LicenseType = "MIT License",
					RepoUrl = "https://github.com/davidshimjs/qrcodejs",
				},
				new License
				{
					Id = 13,
					LicenseName = "Login Form 18 by Colorlib Modify by TioaTyan.",
					LicenseType = "CC BY 3.0",
					RepoUrl = "https://colorlib.com/wp/template/login-form-v18/",
				},
				new License
				{
					Id = 14,
					LicenseName = "LibMan",
					LicenseType = "Apache Software License 2.0",
					RepoUrl = "https://github.com/aspnet/LibraryManager",
				},
			};
		}

		public IEnumerable<License> GetAll()
		{
			return _licenses;
		}

	}
}
