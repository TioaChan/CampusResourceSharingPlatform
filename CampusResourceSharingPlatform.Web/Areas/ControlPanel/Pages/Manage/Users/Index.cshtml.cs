using CampusResourceSharingPlatform.Model.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Users
{
	public class IndexModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public IEnumerable<ApplicationUser> Users { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		public IndexModel(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		private async Task LoadUsersAsync()
		{
			//Users = _userManager.Users.ToList().Where(p => p.DeletedMark = false);
			Users = await _userManager.Users.Where(p => p.DeletedMark == false).ToListAsync();
		}

		public async Task<IActionResult> OnGetAsync()
		{
			await LoadUsersAsync();
			return Page();
		}
		public async Task<IActionResult> OnPostDeleteUserAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			user.DeletedMark = true;
			var result = await _userManager.UpdateAsync(user);
			StatusMessage = result.Succeeded ? "Success：用户删除成功。" : "Error：用户删除失败。";
			await LoadUsersAsync();
			return Page();
		}
	}
}
