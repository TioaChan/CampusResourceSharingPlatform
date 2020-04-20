using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.Posts.Pages
{
	public class TakeExpressModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMissionService<Express> _takeExpressService;
		private readonly IExpressCompanyListService<ExpressCompanyList> _expressCompanyListService;

		public TakeExpressModel(UserManager<ApplicationUser> userManager,
			IMissionService<Express> takeExpressService,
			IExpressCompanyListService<ExpressCompanyList> expressCompanyListService)
		{
			_userManager = userManager;
			_takeExpressService = takeExpressService;
			_expressCompanyListService = expressCompanyListService;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public Express ExpressPost { get; set; }

		public string CurrentUserId { get; set; }

		public ApplicationUser CurrentUser { get; set; }

		public bool StudentIdentityConfirmed { get; set; }

		[BindProperty]
		public string PostId { get; set; }

		public List<ExpressCompanyList> ExpressCompanyList { get; set; }

		private async Task LoadAsync(string postId)
		{
			CurrentUser = await _userManager.GetUserAsync(User);
			ExpressPost = await _takeExpressService.GetActiveMissionById(postId);
			if (CurrentUser == null || postId == null || ExpressPost == null) return;
			ExpressCompanyList = await _expressCompanyListService.GetAllAsync();
			StudentIdentityConfirmed = CurrentUser.StudentIdentityConfirmed;
			CurrentUserId = CurrentUser.Id;
			PostId = ExpressPost.Id;
		}

		public async Task<IActionResult> OnGetAsync(string postId)
		{
			await LoadAsync(postId);
			if (ExpressPost == null || CurrentUser == null)
			{
				return RedirectToPage("Index");
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAcceptMissionAsync(string postId)
		{
			await LoadAsync(postId);
			if (ExpressPost == null || CurrentUser == null || PostId == null)
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
			if (ExpressPost == null || CurrentUser == null || PostId == null)
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

		public async Task<IActionResult> OnPostDeleteMissionAsync(string postId)
		{
			await LoadAsync(postId);
			if (ExpressPost == null || CurrentUser == null || PostId == null)
			{
				return RedirectToPage("Index");
			}
			ExpressPost.DeletedMark = true;
			var result = _takeExpressService.Update(ExpressPost);
			if (result == 1)
			{
				StatusMessage = "Success:删除成功";
				return RedirectToPage("Index");
			}
			StatusMessage = "Error:删除失败，请重试";
			return Page();
		}
	}
}
