using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampusResourceSharingPlatform.Web.Areas.i.Pages
{
	public class TakeExpressModel : PageModel
	{
		[TempData]
		public string StatusMessage { get; set; }
		public void OnGet()
		{
		}
	}
}
