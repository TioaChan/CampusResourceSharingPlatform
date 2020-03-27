using System.Collections.Generic;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Model.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Role
{
	public class IndexModel : PageModel
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public IndexModel(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
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

			public int UserCount { get; set; }

			public List<string> UserInRoleName { get; set; }
		}


		private async Task LoadRoles()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			var users= await _userManager.Users.ToListAsync();
			foreach (var role in roles)
			{
				var userInRoleNames = new List<string>();
				foreach (var user in users)
				{
					if (await _userManager.IsInRoleAsync(user, role.Name))//用户属于该角色
					{
						userInRoleNames.Add(user.UserName);
					}
				}
				RolesModel.Add(new RoleModel
				{
					RoleId = role.Id,
					RoleName = role.Name,
					UserCount = (await _userManager.GetUsersInRoleAsync(role.Name)).Count,
					UserInRoleName = userInRoleNames,
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
