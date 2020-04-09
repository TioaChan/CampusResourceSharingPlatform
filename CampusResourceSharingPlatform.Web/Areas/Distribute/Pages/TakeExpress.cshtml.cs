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
	public class TakeExpressModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ITakeExpressService<Express> _takeExpress;

		public TakeExpressModel(UserManager<ApplicationUser> userManager, ITakeExpressService<Express> takeExpress)
		{
			_userManager = userManager;
			_takeExpress = takeExpress;
		}


		[BindProperty]
		public TakeExpressInputModel TakeExpressInput { get; set; }

		[BindProperty]
		public string PostUserId { get; set; }

		[BindProperty]
		public bool EditMark { get; set; }

		[BindProperty]
		public string GoodsUrl { get; set; }

		[BindProperty]
		public string PostId { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		public class TakeExpressInputModel
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
			/// 任务奖励
			/// </summary>
			[Display(Name = "设置酬劳")]
			[Required(ErrorMessage = "设置酬劳 为必填项")]
			public string MissionReward { get; set; }

			/// <summary>
			/// 快递公司
			/// </summary>
			[Display(Name = "快递公司")]
			[Required(ErrorMessage = "快递公司 为必填项")]
			public string ExpressCompany { get; set; }

			[Display(Name = "快递公司")]
			[Required(ErrorMessage = "快递公司 为必选项")]
			public ExpressCompany ListExpressCompany { get; set; }

			/// <summary>
			/// 快递单号
			/// </summary>
			[Display(Name = "快递单号")]
			[Required(ErrorMessage = "快递单号 为必填项")]
			public string TrackingCode { get; set; }

			/// <summary>
			/// 快递收件人姓名
			/// </summary>
			[Display(Name = "收货人姓名")]
			[Required(ErrorMessage = "收货人姓名 为必填项")]
			public string Consignee { get; set; }

			/// <summary>
			/// 快递收件人手机号【后四位？】
			/// </summary>
			[Display(Name = "收货人手机号")]
			[Required(ErrorMessage = "收货人手机号 为必填项")]
			public string ConsigneePhone { get; set; }


			/// <summary>
			/// 菜鸟驿站取货码
			/// </summary>
			[Display(Name = "取货码")]
			[Required(ErrorMessage = "取货码 为必填项")]
			public string PickCode { get; set; }

			/// <summary>
			/// 菜鸟驿站地址
			/// </summary>
			[Display(Name = "驿站地址")]
			[Required(ErrorMessage = "驿站地址 为必填项")]
			public string YiZhanName { get; set; }

			/// <summary>
			/// 包裹重量 单位:千克
			/// </summary>
			[Display(Name = "包裹重量")]
			[Required(ErrorMessage = "包裹重量 为必填项")]
			public double Weight { get; set; }

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

		}


		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null || !user.StudentIdentityConfirmed) return RedirectToPage("Index");
			var post = await _takeExpress.GetLastMissionInfoAsync(user.Id);
			TakeExpressInput = new TakeExpressInputModel
			{
				PostUserId = user.Id,
			};
			if (post != null)
			{
				TakeExpressInput.ReceiveAddress1 = post.PosterAddress1;
				TakeExpressInput.ReceiveAddress2 = post.PosterAddress2;
				TakeExpressInput.ReceivePhoneNumber = post.PosterPhoneNumber;
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
			var post = new Express
			{
				Id = Guid.NewGuid().ToString(),
				ExpressCompany = TakeExpressInput.ListExpressCompany.ToString(),
				TrackingCode = TakeExpressInput.TrackingCode,
				ConsigneePhone = TakeExpressInput.ConsigneePhone,
				Consignee = TakeExpressInput.Consignee,
				PickCode = TakeExpressInput.PickCode,
				YiZhanName = TakeExpressInput.YiZhanName,
				Weight = TakeExpressInput.Weight,
				PosterAddress1 = TakeExpressInput.ReceiveAddress1,
				PosterAddress2 = TakeExpressInput.ReceiveAddress2,
				PosterPhoneNumber = TakeExpressInput.ReceivePhoneNumber,
				TypeId = "00000000-0000-0000-0000-000000000001",
				PostUserId = user.Id,
				PostTime = time,
				InvalidTime = time.AddDays(2.0),
				MissionNotes = TakeExpressInput.MissionNotes,
				MissionReward = TakeExpressInput.MissionReward
			};
			var result = _takeExpress.Post(post);
			if (result == 1)
			{
				StatusMessage = "Success:发布成功";
				return RedirectToPage("/TakeExpress", new { Area = "Posts", postId = post.Id });
			}
			StatusMessage = "Error:发布失败";
			return RedirectToPage();
		}

		public async Task<IActionResult> OnGetEditMissionAsync(string postId)
		{
			var user = await _userManager.GetUserAsync(User);
			var post = await _takeExpress.GetMissionById(postId);
			if (user == null || !user.StudentIdentityConfirmed || post.PostUserId != user.Id || post.DeletedMark) return RedirectToPage("Index");

			TakeExpressInput = new TakeExpressInputModel
			{
				PostUserId = user.Id,
				ExpressCompany = post.ExpressCompany,
				TrackingCode = post.TrackingCode,
				ConsigneePhone = post.ConsigneePhone,
				Consignee = post.Consignee,
				PickCode = post.PickCode,
				YiZhanName = post.YiZhanName,
				Weight = post.Weight,
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
			var post = new Express();
			var time = DateTime.UtcNow;
			post.Id = PostId;
			post.ExpressCompany = TakeExpressInput.ListExpressCompany.ToString();
			post.TrackingCode = TakeExpressInput.TrackingCode;
			post.ConsigneePhone = TakeExpressInput.ConsigneePhone;
			post.Consignee = TakeExpressInput.Consignee;
			post.PickCode = TakeExpressInput.PickCode;
			post.YiZhanName = TakeExpressInput.YiZhanName;
			post.Weight = TakeExpressInput.Weight;
			post.PosterAddress1 = TakeExpressInput.ReceiveAddress1;
			post.PosterAddress2 = TakeExpressInput.ReceiveAddress2;
			post.PosterPhoneNumber = TakeExpressInput.ReceivePhoneNumber;
			post.TypeId = "00000000-0000-0000-0000-000000000001";
			post.PostUserId = user.Id;
			post.PostTime = time;
			post.InvalidTime = time.AddDays(2.0);
			post.MissionNotes = TakeExpressInput.MissionNotes;
			post.MissionReward = TakeExpressInput.MissionReward;
			var result = _takeExpress.Update(post);
			if (result == 1)
			{
				StatusMessage = "Success:修改成功";
				return RedirectToPage("/TakeExpress", new { Area = "Posts", postId = post.Id });
			}
			StatusMessage = "Success:修改失败";
			return Page();
		}
	}
}
