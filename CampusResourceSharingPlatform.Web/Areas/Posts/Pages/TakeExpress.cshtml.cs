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
	public class TakeExpressModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ITakeExpressService<Express> _takeExpressService;

		public TakeExpressModel(UserManager<ApplicationUser> userManager,
			ITakeExpressService<Express> takeExpressService)
		{
			_userManager = userManager;
			_takeExpressService = takeExpressService;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public Express ExpressPost { get; set; }

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
			ExpressPost = await _takeExpressService.GetMissionById(postId);
			StudentIdentityConfirmed = currentUser.StudentIdentityConfirmed;
			CurrentUserId = currentUser.Id;
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
			ExpressPost = await _takeExpressService.GetMissionById(postId);
			StudentIdentityConfirmed = currentUser.StudentIdentityConfirmed;
			CurrentUserId = currentUser.Id;
			ExpressPost.AcceptUserId = CurrentUserId;
			ExpressPost.AcceptTime = DateTime.UtcNow;
			ExpressPost.IsAccepted = true;
			ExpressPost.IsCompleted = false;
			var result = _takeExpressService.Update(ExpressPost);
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
			ExpressPost = await _takeExpressService.GetMissionById(postId);
			StudentIdentityConfirmed = currentUser.StudentIdentityConfirmed;
			CurrentUserId = currentUser.Id;
			ExpressPost.AcceptUserId = null;
			ExpressPost.AcceptTime = null;
			ExpressPost.IsAccepted = false;
			ExpressPost.IsCompleted = false;
			var result = _takeExpressService.Update(ExpressPost);
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
