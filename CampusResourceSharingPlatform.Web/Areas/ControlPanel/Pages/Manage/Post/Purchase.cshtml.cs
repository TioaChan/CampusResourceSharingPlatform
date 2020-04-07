using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Post
{
	[Authorize(Roles = "Administrators")]
	public class PurchaseModel : PageModel
	{
		private readonly IPurchaseService<Purchase> _purchaseService;

		public PurchaseModel(IPurchaseService<Purchase> purchaseService)
		{
			_purchaseService = purchaseService;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public List<Purchase> Posts { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			Posts = await _purchaseService.GetAllActiveMissionAsync();
			return Page();
		}
	}
}
