using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

		public async Task<IActionResult> OnGetAsync(string postId)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (postId == null || currentUser == null)
			{
				return RedirectToPage("Index");
			}
			PurchasePost = await _purchaseService.GetMissionById(postId);
			return Page();
		}
	}
}
