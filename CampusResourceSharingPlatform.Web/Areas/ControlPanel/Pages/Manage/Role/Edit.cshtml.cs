using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Role
{
	public class EditModel : PageModel
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public EditModel(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
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

			public string UserId { get; set; }

			public List<ApplicationUser> UsersIncluded { get; set; }

			public string NewUserId { get; set; }

			public List<ApplicationUser> UserNotIncluded { get; set; }

		}

		private async Task LoadAsync(string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);
			if (role==null)
			{
				return;
			}
			Role = new RoleModel
			{
				Id = role.Id,
				NewRoleName = role.Name,
				UsersIncluded = new List<ApplicationUser>(),
				UserNotIncluded = new List<ApplicationUser>(),
			};

			var users = await _userManager.Users.ToListAsync();
			foreach (var user in users)
			{
				if (await _userManager.IsInRoleAsync(user, role.Name))//用户属于该角色
				{
					Role.UsersIncluded.Add(user);
				}
				else
				{
					Role.UserNotIncluded.Add(user);
				}
			}
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

		public async Task<IActionResult> OnPostAddUserToRoleAsync(string roleId)
		{
			var user = await _userManager.FindByIdAsync(Role.NewUserId);
			var role = await _roleManager.FindByIdAsync(roleId);

			if (user != null && role != null)
			{
				var result = await _userManager.AddToRoleAsync(user, role.Name);
				if (result.Succeeded)
				{
					StatusMessage = "Success:添加用户成功";
					return RedirectToPage("Edit",new {roleId });
				}
			}
			StatusMessage = "Error:添加用户失败";
			return RedirectToPage("Edit", new { roleId });
		}


		public async Task<IActionResult> OnPostRemoveUserFromRoleAsync(string roleId)
		{
			var user = await _userManager.FindByIdAsync(Role.UserId);
			var role = await _roleManager.FindByIdAsync(roleId);

			if (user != null && role != null)
			{
				var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
				if (result.Succeeded)
				{
					StatusMessage = "Success:从角色\""+role.Name+ "\"中移除用户\"" + user.UserName+ "\"成功";
					return RedirectToPage("Edit", new { roleId });
				}
				StatusMessage = "Error:从角色\"" + role.Name + "\"中移除用户\"" + user.UserName + "\"失败";
				return RedirectToPage("Edit", new { roleId });
			}
			StatusMessage = "Error:移除用户失败";
			return RedirectToPage("Edit", new { roleId });
		}
	}
}
