﻿using CampusResourceSharingPlatform.Model.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.Identity.Pages.Account
{
	[AllowAnonymous]
	public class ConfirmEmailChangeModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public ConfirmEmailChangeModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
		{
			if (userId == null || email == null || code == null)
			{
				return RedirectToPage("/Index");
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{userId}'.");
			}

			//code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
			var result = await _userManager.ChangeEmailAsync(user, email, code);
			if (!result.Succeeded)
			{
				StatusMessage = "验证邮箱地址时发生错误。";
				return Page();
			}

			await _signInManager.RefreshSignInAsync(user);
			StatusMessage = "你成功验证了你的邮箱。";
			return Page();
		}
	}
}
