using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Role
{
	public class EditModel : PageModel
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public EditModel(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public RoleModel Role { get; set; }

		public class RoleModel
		{
			[ReadOnly(true)]
			public string Id { get; set; }

			[Required]
			[Display(Name = "New Role Name")]
			public string NewRoleName { get; set; }
		}

		private async Task LoadAsync(string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);
			Role = new RoleModel
			{
				Id = role.Id,
				NewRoleName = role.Name
			};
			
		}

		public async Task<IActionResult> OnGetAsync(string roleId)
		{
			if (string.IsNullOrEmpty(roleId))
			{
				return RedirectToPage("Index");
			}
			await LoadAsync(roleId);
			return Page();
		}

		public async Task<IActionResult> OnPostEditRoleAsync(string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);
			if (Role.NewRoleName==role.Name)
			{
				await LoadAsync(roleId);
				StatusMessage = "this role is unchanged.";
				return Page();
			}
			role.Name = Role.NewRoleName;
			var result = await _roleManager.UpdateAsync(role);
			if (result.Succeeded)
			{
				StatusMessage = "角色修改成功.";
				return RedirectToPage("Index");
			}
			StatusMessage = "this role is unchanged.";
			return RedirectToPage("Index");
		}
	}
}
