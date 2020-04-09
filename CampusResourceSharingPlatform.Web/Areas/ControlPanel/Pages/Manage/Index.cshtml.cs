using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage
{
	[Authorize(Roles = "Administrators")]
	public class IndexModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ITakeExpressService<Express> _takeExpressService;
		private readonly IPurchaseService<Purchase> _purchaseService;
		private readonly IFleaMarketService<SecondHand> _fleaMarketService;
		private readonly IHireService<Hire> _hireService;

		public IndexModel(UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager,
			ITakeExpressService<Express> takeExpressService,
			IPurchaseService<Purchase> purchaseService,
			IFleaMarketService<SecondHand> fleaMarketService,
			IHireService<Hire> hireService)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_takeExpressService = takeExpressService;
			_purchaseService = purchaseService;
			_fleaMarketService = fleaMarketService;
			_hireService = hireService;
		}
		public int UserCount { get; set; }
		public int RoleCount { get; set; }

		public int PostsCount { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			UserCount = await _userManager.Users.CountAsync();
			RoleCount = await _roleManager.Roles.CountAsync();
			PostsCount = (await _takeExpressService.GetAllActiveMissionAsync()).Count
						 + (await _purchaseService.GetAllActiveMissionAsync()).Count
						 + (await _fleaMarketService.GetAllActiveMissionAsync()).Count
						 + (await _hireService.GetAllActiveMissionAsync()).Count;
			return Page();
		}
	}
}
