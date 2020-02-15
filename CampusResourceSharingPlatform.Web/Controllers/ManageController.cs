using Microsoft.AspNetCore.Mvc;

namespace CampusResourceSharingPlatform.Web.Controllers
{
	public class ManageController:Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Role()
		{
			return View();
		}
	}
}
