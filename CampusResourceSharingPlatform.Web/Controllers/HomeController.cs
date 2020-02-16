using System.Diagnostics;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CampusResourceSharingPlatform.Web.Models;
using CampusResourceSharingPlatform.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using CampusResourceSharingPlatform.Model;

namespace CampusResourceSharingPlatform.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ILicensesDateService<License> _licenses;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public HomeController(ILogger<HomeController> logger,
			ILicensesDateService<License> licenses,
			SignInManager<ApplicationUser> signInManager,
			UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_licenses = licenses;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{

			if (_signInManager.IsSignedIn(User))//用户已登录
			{
				//var studentStatus = await _studentQueryService.IsStudentIdentityConfirmed();
				var user = await _userManager.GetUserAsync(User);
				var overviewIndexViewModel = new OverviewIndexViewModel
				{
					StatusMessage = !user.StudentIdentityConfirmed
						? "Error:你还没有验证学生身份，请先去个人设置中验证学生身份。":""
				};
				return View(overviewIndexViewModel);
			}
			else//用户未登录
			{
				return View();
			}
			
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult About()
		{
			var list=_licenses.GetAll();
			return View(list);
		}
	}
}
