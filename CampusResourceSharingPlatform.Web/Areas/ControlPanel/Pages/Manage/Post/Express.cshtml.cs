using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Post
{
	[Authorize(Roles = "Administrators")]
	public class ExpressModel : PageModel
	{
		[TempData]
		public string StatusMessage { get; set; }
		public void OnGet()
		{
		}
	}
}
