using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Role
{
	public class IndexModel : PageModel
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public IEnumerable<IdentityRole> Roles { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		public IndexModel(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		private async Task LoadRoles()
		{
			Roles = await _roleManager.Roles.ToListAsync();
		}

		public async Task<IActionResult> OnGetAsync()
		{
			await LoadRoles();
			return Page();
		}

		public async Task<IActionResult> OnPostDeleteRoleAsync(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);
			if (role!=null)
			{
				var result = await _roleManager.DeleteAsync(role);
				StatusMessage = result.Succeeded ? "Success:角色删除成功" : "Error:角色删除失败";
			}
			else
			{
				StatusMessage = "Error:角色删除失败";
			}
			await LoadRoles();
			return RedirectToPage();
		}
	}
}
