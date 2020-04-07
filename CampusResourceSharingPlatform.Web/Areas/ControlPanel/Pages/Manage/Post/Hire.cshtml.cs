using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Post
{
	[Authorize(Roles = "Administrators")]
	public class HireModel : PageModel
	{
		private readonly IHireService<Hire> _hireService;

		public HireModel(IHireService<Hire> hireService)
		{
			_hireService = hireService;
		}

		public List<Hire> Posts { get; set; }

		[TempData]
		public string StatusMessage { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			Posts = await _hireService.GetAllActiveMissionAsync();
			return Page();
		}
	}
}
