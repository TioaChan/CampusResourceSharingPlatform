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
	public class PurchaseModel : PageModel
	{
		private readonly IMissionService<Purchase> _purchaseService;
		private readonly UserManager<ApplicationUser> _userManager;

		public PurchaseModel(IMissionService<Purchase> purchaseService, UserManager<ApplicationUser> userManager)
		{
			_purchaseService = purchaseService;
			_userManager = userManager;
		}

		public List<Purchase> PurchasePost { get; set; }

		[TempData]
		public string StatusMessage { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			PurchasePost = await _purchaseService.GetAllActiveMissionByPostUserAsync(user);
			return Page();
		}
	}
}
