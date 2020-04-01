using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.Posts.Pages
{
	public class HireModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IHireService<Hire> _hireService;

		public HireModel(UserManager<ApplicationUser> userManager,
			IHireService<Hire> hireService)
		{
			_userManager = userManager;
			_hireService = hireService;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public Hire HirePost { get; set; }

		public string CurrentUserId { get; set; }

		public bool StudentIdentityConfirmed { get; set; }

		public ApplicationUser CurrentUser { get; set; }

		[BindProperty]
		public string PostId { get; set; }

		private async Task LoadAsync(string postId)
		{
			CurrentUser = await _userManager.GetUserAsync(User);
			if (CurrentUser == null || postId == null) return;
			HirePost = await _hireService.GetMissionById(postId);
			StudentIdentityConfirmed = CurrentUser.StudentIdentityConfirmed;
			CurrentUserId = CurrentUser.Id;
			PostId = HirePost.Id;
		}

		public async Task<IActionResult> OnGetAsync(string postId)
		{
			await LoadAsync(postId);
			if (postId == null || CurrentUser == null)
			{
				return RedirectToPage("Index");
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAcceptMissionAsync(string postId)
		{
			await LoadAsync(postId);
			if (postId == null || CurrentUser == null || PostId == null)
			{
				return RedirectToPage("Index");
			}
			HirePost.AcceptUserId = CurrentUserId;
			HirePost.AcceptTime = DateTime.UtcNow;
			HirePost.IsAccepted = true;
			HirePost.IsCompleted = false;
			var result = _hireService.Update(HirePost);
			if (result == 1)
			{
				StatusMessage = "Success:任务领取成功";
				return Page();
			}
			StatusMessage = "Error:任务领取失败,请重试";
			return Page();
		}

		public async Task<IActionResult> OnPostAbortMissionAsync(string postId)
		{
			await LoadAsync(postId);
			if (postId == null || CurrentUser == null || PostId == null)
			{
				return RedirectToPage("Index");
			}
			HirePost.AcceptUserId = null;
			HirePost.AcceptTime = null;
			HirePost.IsAccepted = false;
			HirePost.IsCompleted = false;
			var result = _hireService.Update(HirePost);
			if (result == 1)
			{
				StatusMessage = "Success:放弃任务成功";
				return Page();
			}
			StatusMessage = "Error:任务领取失败,请重试";
			return Page();
		}
	}
}
