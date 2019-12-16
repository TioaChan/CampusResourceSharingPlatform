using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CampusResourceSharingPlatform.Web.Models;
using CampusResourceSharingPlatform.Web.Services;
using CampusResourceSharingPlatform.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

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
				var overviewIndexViewModel = new OverviewIndexViewModel();
				if (!user.StudentIdentityConfirmed)
				{
					overviewIndexViewModel.StatusMessage = "Error:你还没有验证学生身份，请先去验证学生身份。";
				}
				else
				{
					overviewIndexViewModel.StatusMessage = "Success:你已验证学生身份。";
				}
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
