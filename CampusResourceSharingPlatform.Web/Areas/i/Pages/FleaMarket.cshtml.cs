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
	public class FleaMarketModel : PageModel
	{
		private readonly IFleaMarketService<SecondHand> _fleaMarketService;
		private readonly UserManager<ApplicationUser> _userManager;

		public FleaMarketModel(IFleaMarketService<SecondHand> fleaMarketService, UserManager<ApplicationUser> userManager)
		{
			_fleaMarketService = fleaMarketService;
			_userManager = userManager;
		}
		[TempData]
		public string StatusMessage { get; set; }
		public List<SecondHand> FleaMarketPost { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			FleaMarketPost = await _fleaMarketService.GetAllActiveMissionByPostUserAsync(user);
			return Page();
		}
	}
}
