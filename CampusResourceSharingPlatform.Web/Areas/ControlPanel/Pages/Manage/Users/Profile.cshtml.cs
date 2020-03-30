using CampusResourceSharingPlatform.Model.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Users
{
	public class ProfileModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public ApplicationUser CurrentUser { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		public ProfileModel(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<IActionResult> OnGetAsync(string id)
		{
			CurrentUser = await _userManager.FindByIdAsync(id);
			return Page();
		}
	}
}
