using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.i.Pages
{
	[Authorize]
	public class HireModel : PageModel
	{
		private readonly IHireService<Hire> _hireService;
		private readonly UserManager<ApplicationUser> _userManager;

		public HireModel(IHireService<Hire> hireService, UserManager<ApplicationUser> userManager)
		{
			_hireService = hireService;
			_userManager = userManager;
		}
		[TempData]
		public string StatusMessage { get; set; }
		public List<Hire> HirePost { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			HirePost = await _hireService.GetAllActiveMissionByPostUserAsync(user);
			return Page();
		}
	}
}
