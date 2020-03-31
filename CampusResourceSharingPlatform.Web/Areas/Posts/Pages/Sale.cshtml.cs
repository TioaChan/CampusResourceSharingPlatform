using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.Posts.Pages
{
	public class SaleModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IFleaMarketService<SecondHand> _fleaMarketService;

		public SaleModel(UserManager<ApplicationUser> userManager,
			IFleaMarketService<SecondHand> fleaMarketService)
		{
			_userManager = userManager;
			_fleaMarketService = fleaMarketService;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public SecondHand SalePost { get; set; }

		public async Task<IActionResult> OnGetAsync(string postId)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (postId == null || currentUser == null)
			{
				return RedirectToPage("Index");
			}
			SalePost = await _fleaMarketService.GetMissionById(postId);
			return Page();
		}
	}
}
