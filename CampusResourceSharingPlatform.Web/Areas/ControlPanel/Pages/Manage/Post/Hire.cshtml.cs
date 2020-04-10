using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
		private readonly UserManager<ApplicationUser> _userManager;

		public HireModel(IHireService<Hire> hireService, UserManager<ApplicationUser> userManager)
		{
			_hireService = hireService;
			_userManager = userManager;
		}

		public List<Hire> Posts { get; set; }
		public bool SingleUserMark { get; set; }
		public ApplicationUser QueriedUser { get; set; }

		[TempData]
		public string StatusMessage { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			Posts = await _hireService.GetAllActiveMissionAsync();
			SingleUserMark = false;
			return Page();
		}
		public async Task<IActionResult> OnGetSingleUserAsync(string userId)
		{
			QueriedUser = await _userManager.FindByIdAsync(userId);
			Posts = await _hireService.GetAllActiveMissionByPostUserAsync(QueriedUser);
			SingleUserMark = true;
			return Page();
		}
	}
}
