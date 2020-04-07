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
	public class FleaMarketModel : PageModel
	{
		private readonly IFleaMarketService<SecondHand> _fleaMarketService;

		public FleaMarketModel(IFleaMarketService<SecondHand> fleaMarketService)
		{
			_fleaMarketService = fleaMarketService;
		}

		public List<SecondHand> Posts;
		[TempData]
		public string StatusMessage { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			Posts = await _fleaMarketService.GetAllActiveMissionAsync();
			return Page();
		}
	}
}
