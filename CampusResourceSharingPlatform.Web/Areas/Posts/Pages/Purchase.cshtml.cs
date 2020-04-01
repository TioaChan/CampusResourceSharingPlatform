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
	public class PurchaseModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IPurchaseService<Purchase> _purchaseService;

		public PurchaseModel(UserManager<ApplicationUser> userManager,
			IPurchaseService<Purchase> purchaseService)
		{
			_userManager = userManager;
			_purchaseService = purchaseService;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public Purchase PurchasePost { get; set; }

		public ApplicationUser CurrentUser { get; set; }

		public string CurrentUserId { get; set; }

		public bool StudentIdentityConfirmed { get; set; }

		[BindProperty]
		public string PostId { get; set; }

		private async Task LoadAsync(string postId)
		{
			CurrentUser = await _userManager.GetUserAsync(User);
			if (CurrentUser == null || postId == null) return;
			PurchasePost = await _purchaseService.GetMissionById(postId);
			StudentIdentityConfirmed = CurrentUser.StudentIdentityConfirmed;
			CurrentUserId = CurrentUser.Id;
			PostId = PurchasePost.Id;
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
			PurchasePost.AcceptUserId = CurrentUserId;
			PurchasePost.AcceptTime = DateTime.UtcNow;
			PurchasePost.IsAccepted = true;
			PurchasePost.IsCompleted = false;
			var result = _purchaseService.Update(PurchasePost);
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
			PurchasePost.AcceptUserId = null;
			PurchasePost.AcceptTime = null;
			PurchasePost.IsAccepted = false;
			PurchasePost.IsCompleted = false;
			var result = _purchaseService.Update(PurchasePost);
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
