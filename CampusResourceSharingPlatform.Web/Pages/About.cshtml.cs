using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Pages
{
	public class AboutModel : PageModel
	{
		private readonly ILogger<AboutModel> _logger;
		private readonly ILicensesDateService<License> _licenses;

		public AboutModel(ILogger<AboutModel> logger, ILicensesDateService<License> licenses)
		{
			_logger = logger;
			_licenses = licenses;
		}

		public List<License> Licenses { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			Licenses = await _licenses.GetAllAsync();
			return Page();
		}
	}
}
