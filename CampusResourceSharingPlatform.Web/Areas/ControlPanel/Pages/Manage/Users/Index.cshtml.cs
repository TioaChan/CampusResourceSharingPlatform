using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Users
{
	public class IndexModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public List<ApplicationUser> Roles { get; set; }


		public IndexModel(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public IActionResult OnGetAsync()
		{
			Roles = _userManager.Users.ToList();
			return Page();
		}
	}
}
