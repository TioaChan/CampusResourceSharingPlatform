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
		private readonly IMissionService<Express> _takeExpressService;
		private readonly IMissionService<Purchase> _purchaseService;
		private readonly IMissionService<SecondHand> _fleaMarketService;
		private readonly IMissionService<Hire> _hireService;

		public IndexModel(UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager,
			IMissionService<Express> takeExpressService,
			IMissionService<Purchase> purchaseService,
			IMissionService<SecondHand> fleaMarketService,
			IMissionService<Hire> hireService)
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
