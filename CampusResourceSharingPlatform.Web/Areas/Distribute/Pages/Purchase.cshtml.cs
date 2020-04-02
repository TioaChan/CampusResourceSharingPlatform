using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.Distribute.Pages
{
	public class PurchaseModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IPurchaseService<Purchase> _purchase;

		public PurchaseModel(UserManager<ApplicationUser> userManager, IPurchaseService<Purchase> purchase)
		{
			_userManager = userManager;
			_purchase = purchase;
		}

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public PurchaseInputModel PurchaseInput { get; set; }

		[BindProperty]
		public string PostUserId { get; set; }

		[BindProperty]
		public bool EditMark { get; set; }

		[BindProperty]
		public string PostId { get; set; }

		public class PurchaseInputModel
		{
			public string MissionName { get; set; }

			/// <summary>
			/// 所属类目编号
			/// </summary>
			public string TypeId { get; set; }

			/// <summary>
			/// 发布人Id
			/// </summary>
			public string PostUserId { get; set; }

			/// <summary>
			/// 任务发布时间
			/// </summary>
			public DateTime PostTime { get; set; }

			/// <summary>
			/// 任务设定的失效时间
			/// </summary>
			public DateTime InvalidTime { get; set; }

			/// <summary>
			/// 任务备注
			/// </summary>
			[Display(Name = "备注信息")]
			public string MissionNotes { get; set; }

			/// <summary>
			/// 地址1
			/// </summary>
			[Display(Name = "联系地址1")]
			[Required(ErrorMessage = "联系地址1 为必填项")]
			public string ReceiveAddress1 { get; set; }

			/// <summary>
			/// 地址2
			/// </summary>
			[Display(Name = "联系地址2")]
			[Required(ErrorMessage = "联系地址2 为必填项")]
			public string ReceiveAddress2 { get; set; }

			[Display(Name = "联系方式")]
			[Required(ErrorMessage = "联系方式 为必填项")]
			public string ReceivePhoneNumber { get; set; }

			/// <summary>
			/// 购买内容
			/// </summary>
			[Display(Name = "购买内容")]
			[Required(ErrorMessage = "购买内容 为必填项")]
			public string PurchaseContent { get; set; }

			/// <summary>
			/// 购买地址
			/// </summary>
			[Display(Name = "购买地址")]
			[Required(ErrorMessage = "购买地址 为必填项")]
			public string PurchaseAddress { get; set; }

			/// <summary>
			/// 购买需求
			/// </summary>
			[Display(Name = "购买要求")]
			[Required(ErrorMessage = "购买要求 为必填项")]
			public string PurchaseRequirement { get; set; }

			/// <summary>
			/// 任务奖励
			/// </summary>
			[Display(Name = "设置酬劳")]
			[Required(ErrorMessage = "设置酬劳 为必填项")]
			public string MissionReward { get; set; }
		}

		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null || !user.StudentIdentityConfirmed) return RedirectToPage("Index");
			var post = await _purchase.GetLastMissionInfoAsync(user.Id);
			PurchaseInput = new PurchaseInputModel
			{
				PostUserId = user.Id,
			};
			if (post != null)
			{
				PurchaseInput.ReceiveAddress1 = post.PosterAddress1;
				PurchaseInput.ReceiveAddress2 = post.PosterAddress2;
				PurchaseInput.ReceivePhoneNumber = post.PosterPhoneNumber;
			}
			PostUserId = user.Id;
			EditMark = false;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null || !user.StudentIdentityConfirmed || user.Id != PostUserId) return RedirectToPage("Index");
			var time = DateTime.UtcNow;
			var post = new Purchase
			{
				Id = Guid.NewGuid().ToString(),
				PurchaseContent = PurchaseInput.PurchaseContent,
				PurchaseAddress = PurchaseInput.PurchaseAddress,
				PurchaseRequirement = PurchaseInput.PurchaseRequirement,
				TypeId = "00000000-0000-0000-0000-000000000002",
				PostUserId = user.Id,
				PostTime = time,
				InvalidTime = time.AddDays(2.0),
				MissionNotes = PurchaseInput.MissionNotes,
				MissionReward = PurchaseInput.MissionReward,
				PosterAddress1 = PurchaseInput.ReceiveAddress1,
				PosterAddress2 = PurchaseInput.ReceiveAddress2,
				PosterPhoneNumber = PurchaseInput.ReceivePhoneNumber,
			};
			var result = _purchase.Post(post);
			if (result == 1)
			{
				StatusMessage = "Success:发布成功";
				return RedirectToPage("/Purchase", new { Area = "Posts", postId = post.Id });
			}
			StatusMessage = "Error:发布失败";
			return RedirectToPage();
		}

		public async Task<IActionResult> OnGetEditMissionAsync(string postId)
		{
			var user = await _userManager.GetUserAsync(User);
			var post = await _purchase.GetMissionById(postId);
			if (user == null || !user.StudentIdentityConfirmed || post.PostUserId != user.Id || post.DeletedMark) return RedirectToPage("Index");
			PurchaseInput = new PurchaseInputModel
			{
				PostUserId = user.Id,
				PurchaseContent = post.PurchaseContent,
				PurchaseAddress = post.PurchaseAddress,
				PurchaseRequirement = post.PurchaseRequirement,
				MissionNotes = post.MissionNotes,
				ReceiveAddress1 = post.PosterAddress1,
				ReceiveAddress2 = post.PosterAddress2,
				ReceivePhoneNumber = post.PosterPhoneNumber,
				MissionReward = post.MissionReward
			};
			PostId = post.Id;
			PostUserId = user.Id;
			EditMark = true;
			return Page();
		}

		public async Task<IActionResult> OnPostEditMissionAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null || !user.StudentIdentityConfirmed || user.Id != PostUserId || PostId == null) return RedirectToPage("Index");
			var post = new Purchase();
			var time = DateTime.UtcNow;
			post.Id = PostId;
			post.PurchaseContent = PurchaseInput.PurchaseContent;
			post.PurchaseAddress = PurchaseInput.PurchaseAddress;
			post.PurchaseRequirement = PurchaseInput.PurchaseRequirement;
			post.TypeId = "00000000-0000-0000-0000-000000000002";
			post.PostUserId = user.Id;
			post.PostTime = time;
			post.InvalidTime = time.AddDays(2.0);
			post.MissionNotes = PurchaseInput.MissionNotes;
			post.MissionReward = PurchaseInput.MissionReward;
			post.PosterAddress1 = PurchaseInput.ReceiveAddress1;
			post.PosterAddress2 = PurchaseInput.ReceiveAddress2;
			post.PosterPhoneNumber = PurchaseInput.ReceivePhoneNumber;
			var result = _purchase.Update(post);
			if (result == 1)
			{
				StatusMessage = "Success:修改成功";
				return RedirectToPage("/Purchase", new { Area = "Posts", postId = post.Id });
			}
			StatusMessage = "Success:修改失败";
			return Page();
		}
	}
}
