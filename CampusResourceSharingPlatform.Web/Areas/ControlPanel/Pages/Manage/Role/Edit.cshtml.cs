using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Model;
using CampusResourceSharingPlatform.Web.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
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

		public IdentityRole Role { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel
		{
			[ReadOnly(true)]
			public string Id { get; set; }

			[Required]
			[Display(Name = "New Role Name")]
			public string NewRoleName { get; set; }
		}

		private async Task LoadAsync(string id)
		{
			Role = await _roleManager.FindByIdAsync(id);
			Input = new InputModel
			{
				Id = Role.Id,
				NewRoleName = Role.Name
			};
			
		}

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return RedirectToPage("Index");
			}
			await LoadAsync(id);
			return Page();
		}

		public async Task<IActionResult> OnPostEditRoleAsync(string id)
		{
			Role = await _roleManager.FindByIdAsync(id);
			if (Input.NewRoleName==Role.Name)
			{
				await LoadAsync(id);
				StatusMessage = "this role is unchanged.";
				return Page();
			}
			Role.Name = Input.NewRoleName;
			var result = await _roleManager.UpdateAsync(Role);
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
