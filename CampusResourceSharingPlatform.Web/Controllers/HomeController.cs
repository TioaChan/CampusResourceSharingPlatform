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

		public HomeController(ILogger<HomeController> logger,ILicensesDateService<License> licenses,SignInManager<ApplicationUser> signInManager)
		{
			_logger = logger;
			_licenses = licenses;
			_signInManager = signInManager;
		}

		public IActionResult Index()
		{
			if (_signInManager.IsSignedIn(User))//用户已登录
			{
				var overviewIndexViewModel = new OverviewIndexViewModel
				{
					StatusMessage = "Error:Your email is changed."
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
