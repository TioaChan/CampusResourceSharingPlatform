using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Role
{
	[Authorize(Roles = "Administrators")]
	public class AddModel : PageModel
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		public AddModel(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel
		{
			[Required]
			[Display(Name = "Role Name")]
			public string RoleName { get; set; }
		}

		public IActionResult OnGetAsync()
		{
			return Page();
		}

		public async Task<IActionResult> OnPostAddRoleAsync()
		{
			var role = new IdentityRole
			{
				Name = Input.RoleName
			};
			var result = await _roleManager.CreateAsync(role);
			StatusMessage = result.Succeeded ? "Success:角色添加成功" : "Error:角色添加失败";
			return RedirectToPage("Index");
		}
	}
}
