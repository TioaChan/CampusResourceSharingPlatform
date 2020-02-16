using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CampusResourceSharingPlatform.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CampusResourceSharingPlatform.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        

        public LoginModel(SignInManager<ApplicationUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
			[Required(ErrorMessage = "用户名为必填项。")]
			[Display(Name = "用户名")]
			public string UserName { get; set; }

//			[Required(ErrorMessage = "邮箱地址为必填项。")]
//			[EmailAddress]
//			[Display(Name = "邮箱地址")]
//			public string Email { get; set; }

            [Required(ErrorMessage = "密码为必填项")]
            [DataType(DataType.Password)]
			[Display(Name = "密码")]
            public string Password { get; set; }

            [Display(Name = "记住我？")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true

                var user = await _userManager.FindByNameAsync(Input.UserName);
                if (user!=null)
                {
	                var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
	                {
		                _logger.LogInformation("User logged in.");
		                return LocalRedirect(returnUrl);
	                }
	                if (result.RequiresTwoFactor)
	                {
		                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
	                }
	                if (result.IsLockedOut)
	                {
		                _logger.LogWarning("User account locked out.");
		                return RedirectToPage("./Lockout");
	                }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

//        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }
//
//            var user = await _userManager.FindByEmailAsync(Input.Email);
//            if (user == null)
//            {
//                ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
//            }
//
//            var userId = await _userManager.GetUserIdAsync(user);
//            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
//            var callbackUrl = Url.Page(
//                "/Account/ConfirmEmail",
//                pageHandler: null,
//                values: new { userId = userId, code = code },
//                protocol: Request.Scheme);
//            await _emailSender.SendEmailAsync(
//                Input.Email,
//                "Confirm your email",
//                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
//
//            ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
//            return Page();
//        }
    }
}
