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
	public class FleaMarketModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IFleaMarketService<SecondHand> _fleaMarketService;

		public FleaMarketModel(UserManager<ApplicationUser> userManager, IFleaMarketService<SecondHand> fleaMarketService)
		{
			_userManager = userManager;
			_fleaMarketService = fleaMarketService;
		}

		public List<SecondHand> Posts;
		public bool SingleUserMark { get; set; }
		public ApplicationUser queriedUser { get; set; }
		[TempData]
		public string StatusMessage { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			Posts = await _fleaMarketService.GetAllActiveMissionAsync();
			SingleUserMark = false;
			return Page();
		}

		public async Task<IActionResult> OnGetSingleUserAsync(string userId)
		{
			queriedUser = await _userManager.FindByIdAsync(userId);
			Posts = await _fleaMarketService.GetAllActiveMissionByPostUserAsync(queriedUser);
			SingleUserMark = true;
			return Page();
		}
	}
}
