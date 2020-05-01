using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMissionService<Express> _expressService;
		private readonly IMissionService<SecondHand> _fleaMarketService;
		private readonly IMissionService<Hire> _hireService;
		private readonly IMissionService<Purchase> _purchaseService;

		public IndexModel(ILogger<IndexModel> logger,
			SignInManager<ApplicationUser> signInManager,
			UserManager<ApplicationUser> userManager,
			IMissionService<Express> expressService,
			IMissionService<SecondHand> fleaMarketService,
			IMissionService<Hire> hireService,
			IMissionService<Purchase> purchaseService
			)
		{
			_logger = logger;
			_signInManager = signInManager;
			_userManager = userManager;
			_expressService = expressService;
			_fleaMarketService = fleaMarketService;
			_hireService = hireService;
			_purchaseService = purchaseService;
		}


		[BindProperty]
		public IndexPageModel IndexPage { get; set; }

		public class IndexPageModel
		{
			public string UserName { get; set; }

			public bool IsShowNavFunction { get; set; }
			[TempData]
			public string IndexPageStatusMessage { get; set; }

			public List<Express> Expresses { get; set; }

			public List<Purchase> Purchases { get; set; }

			public List<SecondHand> SecondHands { get; set; }

			public List<Hire> Hires { get; set; }
		}

		public async Task<IActionResult> OnGetAsync()
		{
			if (!_signInManager.IsSignedIn(User)) return Page();
			var user = await _userManager.GetUserAsync(User);
			IndexPage = new IndexPageModel
			{
				UserName = user.NickName,
				IsShowNavFunction = user.StudentIdentityConfirmed,
				IndexPageStatusMessage = !user.StudentIdentityConfirmed ? "Error:你还没有验证学生身份，无法进行下单，请先去个人设置中验证学生身份。" : "",
				Expresses = await _expressService.GetTop10ActiveMissionAsync(),
				Purchases = await _purchaseService.GetTop10ActiveMissionAsync(),
				SecondHands = await _fleaMarketService.GetTop10ActiveMissionAsync(),
				Hires = await _hireService.GetTop10ActiveMissionAsync(),
			};
			return Page();
		}
	}
}
