using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.Posts.Pages
{
	public class HireModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IHireService<Hire> _hireService;

		public HireModel(UserManager<ApplicationUser> userManager,
			IHireService<Hire> hireService)
		{
			_userManager = userManager;
			_hireService = hireService;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public Hire HirePost { get; set; }

		public async Task<IActionResult> OnGetAsync(string postId)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (postId == null || currentUser == null)
			{
				return RedirectToPage("Index");
			}
			HirePost = await _hireService.GetMissionById(postId);
			return Page();
		}
	}
}
