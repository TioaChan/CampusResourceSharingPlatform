using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.Posts.Pages
{
	public class PurchaseModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IPurchaseService<Purchase> _purchaseService;

		public PurchaseModel(UserManager<ApplicationUser> userManager,
			IPurchaseService<Purchase> purchaseService)
		{
			_userManager = userManager;
			_purchaseService = purchaseService;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public Purchase PurchasePost { get; set; }

		public string CurrentUserId { get; set; }

		public bool StudentIdentityConfirmed { get; set; }

		public async Task<IActionResult> OnGetAsync(string postId)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (postId == null || currentUser == null)
			{
				return RedirectToPage("Index");
			}
			PurchasePost = await _purchaseService.GetMissionById(postId);
			StudentIdentityConfirmed = currentUser.StudentIdentityConfirmed;
			CurrentUserId = currentUser.Id;
			return Page();
		}
	}
}
