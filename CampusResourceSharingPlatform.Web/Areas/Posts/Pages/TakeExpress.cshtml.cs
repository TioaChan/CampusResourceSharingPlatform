using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.Posts.Pages
{
	public class TakeExpressModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ITakeExpressService<Express> _takeExpressService;

		public TakeExpressModel(UserManager<ApplicationUser> userManager,
			ITakeExpressService<Express> takeExpressService)
		{
			_userManager = userManager;
			_takeExpressService = takeExpressService;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public Express ExpressPost { get; set; }

		public async Task<IActionResult> OnGetAsync(string postId)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (postId == null || currentUser == null)
			{
				return RedirectToPage("Index");
			}
			ExpressPost = await _takeExpressService.GetMissionById(postId);
			return Page();
		}
	}
}
