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
	public class PurchaseModel : PageModel
	{
		private readonly IMissionService<Purchase> _purchaseService;
		private readonly UserManager<ApplicationUser> _userManager;

		public PurchaseModel(IMissionService<Purchase> purchaseService, UserManager<ApplicationUser> userManager)
		{
			_purchaseService = purchaseService;
			_userManager = userManager;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public List<Purchase> Posts { get; set; }
		public bool SingleUserMark { get; set; }
		public ApplicationUser QueriedUser { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			Posts = await _purchaseService.GetAllActiveMissionAsync();
			SingleUserMark = false;
			return Page();
		}
		public async Task<IActionResult> OnGetSingleUserAsync(string userId)
		{
			QueriedUser = await _userManager.FindByIdAsync(userId);
			Posts = await _purchaseService.GetAllActiveMissionByPostUserAsync(QueriedUser);
			SingleUserMark = true;
			return Page();
		}
	}
}
