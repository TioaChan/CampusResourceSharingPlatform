using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Post
{
	public class FleaMarketModel : PageModel
	{
		[TempData]
		public string StatusMessage { get; set; }
		public void OnGet()
		{
		}
	}
}
