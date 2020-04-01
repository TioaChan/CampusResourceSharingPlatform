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
	public class SaleModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IFleaMarketService<SecondHand> _fleaMarketService;

		public SaleModel(UserManager<ApplicationUser> userManager,
			IFleaMarketService<SecondHand> fleaMarketService)
		{
			_userManager = userManager;
			_fleaMarketService = fleaMarketService;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public SecondHand SalePost { get; set; }

		public string CurrentUserId { get; set; }

		public bool StudentIdentityConfirmed { get; set; }

		public ApplicationUser CurrentUser { get; set; }

		[BindProperty]
		public string PostId { get; set; }

		private async Task LoadAsync(string postId)
		{
			CurrentUser = await _userManager.GetUserAsync(User);
			if (CurrentUser == null || postId == null) return;
			SalePost = await _fleaMarketService.GetMissionById(postId);
			StudentIdentityConfirmed = CurrentUser.StudentIdentityConfirmed;
			CurrentUserId = CurrentUser.Id;
			PostId = SalePost.Id;
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
			SalePost.AcceptUserId = CurrentUserId;
			SalePost.AcceptTime = DateTime.UtcNow;
			SalePost.IsAccepted = true;
			SalePost.IsCompleted = false;
			var result = _fleaMarketService.Update(SalePost);
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
			SalePost.AcceptUserId = null;
			SalePost.AcceptTime = null;
			SalePost.IsAccepted = false;
			SalePost.IsCompleted = false;
			var result = _fleaMarketService.Update(SalePost);
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
