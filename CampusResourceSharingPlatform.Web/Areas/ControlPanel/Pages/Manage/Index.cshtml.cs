using System.Threading.Tasks;
using CampusResourceSharingPlatform.Model.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage
{
	public class IndexModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public IndexModel(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public int UserCount { get; set; }
		public int RoleCount { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			UserCount =await _userManager.Users.CountAsync();
			RoleCount = await _roleManager.Roles.CountAsync();
			return Page();
		}
	}
}
