using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Post
{
	[Authorize(Roles = "Administrators")]
	public class ExpressModel : PageModel
	{
		private readonly ITakeExpressService<Express> _takeExpressService;
		private readonly UserManager<ApplicationUser> _userManager;

		public ExpressModel(ITakeExpressService<Express> takeExpressService, UserManager<ApplicationUser> userManager)
		{
			_takeExpressService = takeExpressService;
			_userManager = userManager;
		}
		public List<Express> Posts { get; set; }

		[TempData]
		public string StatusMessage { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			Posts = await _takeExpressService.GetAllActiveMissionAsync();
			return Page();
		}
	}
}
