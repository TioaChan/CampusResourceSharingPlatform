using CampusResourceSharingPlatform.Interface;
using System.Collections.Generic;
using CampusResourceSharingPlatform.Model;

namespace CampusResourceSharingPlatform.Service
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
		}

		public IEnumerable<License> GetAll()
		{
			return _licenses;
		}

	}
}
