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

		public ApplicationUser CurrentUser { get; set; }

		public bool StudentIdentityConfirmed { get; set; }

		[BindProperty]
		public string PostId { get; set; }

		private async Task LoadAsync(string postId)
		{
			CurrentUser = await _userManager.GetUserAsync(User);
			if (CurrentUser == null || postId == null) return;
			ExpressPost = await _takeExpressService.GetMissionById(postId);
			StudentIdentityConfirmed = CurrentUser.StudentIdentityConfirmed;
			CurrentUserId = CurrentUser.Id;
			PostId = ExpressPost.Id;
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
			await LoadAsync(postId);
			if (postId == null || CurrentUser == null || PostId == null)
			{
				return RedirectToPage("Index");
			}
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
