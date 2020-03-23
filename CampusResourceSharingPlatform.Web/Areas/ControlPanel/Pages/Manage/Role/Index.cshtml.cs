using System.Collections.Generic;
using System.Linq;
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

		public IndexModel(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
			RolesModel=new List<RoleModel>();
		}

		[TempData]
		public string StatusMessage { get; set; }


		[BindProperty]
		public List<RoleModel> RolesModel { get; set; }

		public class RoleModel
		{
			public string RoleId { get; set; }

			public string RoleName { get; set; }
		}


		private async Task LoadRoles()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			foreach (var role in roles)
			{
				RolesModel.Add(new RoleModel
				{
					RoleId = role.Id,
					RoleName = role.Name
				});
			}
		}

		public async Task<IActionResult> OnGetAsync()
		{
			await LoadRoles();
			return Page();
		}

		public async Task<IActionResult> OnPostDeleteRoleAsync(string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);
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
