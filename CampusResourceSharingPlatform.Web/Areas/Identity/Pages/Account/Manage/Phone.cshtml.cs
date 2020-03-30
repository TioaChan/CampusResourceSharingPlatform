using CampusResourceSharingPlatform.Model.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.Identity.Pages.Account.Manage
{
	public class PhoneModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public PhoneModel(
			UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public string Phone { get; set; }

		public bool IsPhoneSubmited { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel
		{
			[Required]
			[Display(Name = "New Phone Number")]
			public string NewPhone { get; set; }
		}

		private async Task LoadAsync(ApplicationUser user)
		{
			var phone = await _userManager.GetPhoneNumberAsync(user);
			Phone = phone;
			Input = new InputModel
			{
				NewPhone = phone,
			};
			IsPhoneSubmited = await _userManager.GetPhoneNumberAsync(user) != null;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			await LoadAsync(user);
			return Page();
		}

		public async Task<IActionResult> OnPostChangePhoneNumAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			if (!ModelState.IsValid)
			{
				await LoadAsync(user);
				return Page();
			}

			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
			if (Input.NewPhone != phoneNumber)
			{
				var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, Input.NewPhone);
				var result = await _userManager.ChangePhoneNumberAsync(user, Input.NewPhone, code);
				if (!result.Succeeded)
				{
					var userId = await _userManager.GetUserIdAsync(user);
					throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
				}
				await _signInManager.RefreshSignInAsync(user);
				StatusMessage = "Your Phone Number is changed.";
				IsPhoneSubmited = true;
				return RedirectToPage();
			}
			await _signInManager.RefreshSignInAsync(user);
			StatusMessage = "Your Phone Number is unchanged.";
			return RedirectToPage();
		}
	}
}
