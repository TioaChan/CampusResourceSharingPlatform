using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace CampusResourceSharingPlatform.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _iWebHostEnvironment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment iWebHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _iWebHostEnvironment = iWebHostEnvironment;
        }

        public string Username { get; set; }
        public string AvatarFileName { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        [Display(Name = "Avatar")]
        public IFormFile NewAvatar { get; set; }

		public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

		private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
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

        public async Task<IActionResult> OnPostPhoneNumberAsync()
        {
			var user = await _userManager.GetUserAsync(User);
			//判断表达合法
			if (!ModelState.IsValid)
			{
				await LoadAsync(user);
				return Page();
			}

			if (Input.PhoneNumber==null)
			{
				ModelState.AddModelError("Input","Please enter your Phone Number.");
				StatusMessage = "Your profile has no changes";
				return RedirectToPage();
			}

			//判断用户是否存在
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
			if (Input.PhoneNumber != phoneNumber)
			{
				var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
				if (!setPhoneResult.Succeeded)
				{
					var userId = await _userManager.GetUserIdAsync(user);
					throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
				}
			}
			await _signInManager.RefreshSignInAsync(user);
			StatusMessage = "Your profile has been updated";
			return RedirectToPage();
        }

		public async Task<IActionResult> OnPostAvatarAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			//判断表达合法
			if (!ModelState.IsValid)
			{
				await LoadAsync(user);
				return Page();
			}
			//判断用户是否存在
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			if (NewAvatar != null)
			{
				var uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "avatar", user.UserName);
				AvatarFileName = Guid.NewGuid() + Path.GetExtension(NewAvatar.FileName);
				var filePath = Path.Combine(uploadFolder, AvatarFileName);
				if (!Directory.Exists(uploadFolder))
				{
					Directory.CreateDirectory(uploadFolder);
				}
				await NewAvatar.CopyToAsync(new FileStream(filePath, FileMode.Create));
				try
				{
					user.ProfilePhotoUrl = "/avatar/" + user.UserName + "/" + AvatarFileName;
					var result = await _userManager.UpdateAsync(user);
					if (!result.Succeeded)
					{
						StatusMessage = "Error：图片上传失败，请重试。";
					}
					StatusMessage = "Success：图片上传成功。";
				}
				catch
				{
					StatusMessage = "Error：不可预料到的错误，请重试。";
					return RedirectToPage();
				}
			}
			StatusMessage = "Error：不可预料到的错误，请重试。";
			return RedirectToPage();
		}
	}
}
