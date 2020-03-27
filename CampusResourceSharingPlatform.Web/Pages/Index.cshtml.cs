using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CampusResourceSharingPlatform.Web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public IndexModel(ILogger<IndexModel> logger,
			SignInManager<ApplicationUser> signInManager,
			UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_signInManager = signInManager;
			_userManager = userManager;
		}


		[BindProperty]
		public IndexPageModel IndexPage { get; set; }

		public class IndexPageModel
		{
			public string UserName { get; set; }

			public bool IsShowNavFunction { get; set; }
			[TempData]
			public string IndexPageStatusMessage { get; set; }
		}

		public async Task<IActionResult> OnGetAsync()
		{
			if (_signInManager.IsSignedIn(User))
			{
				var user = await _userManager.GetUserAsync(User);
				IndexPage=new IndexPageModel
				{
					UserName = user.NickName,
					IsShowNavFunction = user.StudentIdentityConfirmed,
					IndexPageStatusMessage = !user.StudentIdentityConfirmed ?"Error:你还没有验证学生身份，无法进行下单，请先去个人设置中验证学生身份。" : ""
				};
			}
			return Page();
		}
	}
}
