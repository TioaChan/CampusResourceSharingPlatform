using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Model.Application;
using Microsoft.AspNetCore.Identity;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Post
{
	[Authorize(Roles = "Administrators")]
	public class PurchaseModel : PageModel
	{
		private readonly IPurchaseService<Purchase> _purchaseService;
		private readonly UserManager<ApplicationUser> _userManager;

		public PurchaseModel(IPurchaseService<Purchase> purchaseService,UserManager<ApplicationUser> userManager)
		{
			_purchaseService = purchaseService;
			_userManager = userManager;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public List<Purchase> Posts { get; set; }
		public bool SingleUserMark { get; set; }
		public ApplicationUser queriedUser { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			Posts = await _purchaseService.GetAllActiveMissionAsync();
			SingleUserMark = false;
			return Page();
		}
		public async Task<IActionResult> OnGetSingleUserAsync(string userId)
		{
			queriedUser = await _userManager.FindByIdAsync(userId);
			Posts = await _purchaseService.GetAllActiveMissionByPostUserAsync(queriedUser);
			SingleUserMark = true;
			return Page();
		}
	}
}
