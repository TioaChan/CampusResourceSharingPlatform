using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Role
{
	public class IndexModel : PageModel
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public List<IdentityRole> Roles { get; set; }


		public IndexModel(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}
		public IActionResult OnGetAsync()
		{
			Roles= _roleManager.Roles.ToList();
			return Page();
		}
	}
}
