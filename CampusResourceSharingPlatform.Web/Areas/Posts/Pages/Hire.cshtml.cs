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

		[BindProperty]
		public string PostId { get; set; }

		public async Task<IActionResult> OnGetAsync(string postId)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (postId == null || currentUser == null)
			{
				return RedirectToPage("Index");
			}
			CurrentUserId = currentUser.Id;
			StudentIdentityConfirmed = currentUser.StudentIdentityConfirmed;
			HirePost = await _hireService.GetMissionById(postId);
			PostId = postId;
			return Page();
		}

		public async Task<IActionResult> OnPostAcceptMissionAsync(string postId)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (postId == null || currentUser == null || PostId == null)
			{
				return RedirectToPage("Index");
			}
			HirePost = await _hireService.GetMissionById(postId);
			StudentIdentityConfirmed = currentUser.StudentIdentityConfirmed;
			CurrentUserId = currentUser.Id;
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
			var currentUser = await _userManager.GetUserAsync(User);
			if (postId == null || currentUser == null || PostId == null)
			{
				return RedirectToPage("Index");
			}
			HirePost = await _hireService.GetMissionById(postId);
			StudentIdentityConfirmed = currentUser.StudentIdentityConfirmed;
			CurrentUserId = currentUser.Id;
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
