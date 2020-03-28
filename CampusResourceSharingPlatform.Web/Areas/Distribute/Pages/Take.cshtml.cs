using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampusResourceSharingPlatform.Web.Areas.Distribute.Pages
{
	public class TakeModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ITakeExpressService<Express> _takeExpress;

		public TakeModel(UserManager<ApplicationUser> userManager,ITakeExpressService<Express> takeExpress)
		{
			_userManager = userManager;
			_takeExpress = takeExpress;
		}


		[BindProperty]
		public TakeExpressModel TakeExpress { get; set; }

		[BindProperty]
		public string PostUserId { get; set; }

		public class TakeExpressModel
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
			if (user == null) return RedirectToPage("Index");
			TakeExpress=new TakeExpressModel
			{
				PostUserId = user.Id,
			};
			PostUserId = user.Id;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null) return RedirectToPage("Index");
			if (user.Id!=PostUserId) return RedirectToPage("Index");
			var time = DateTime.UtcNow;
			var post=new Express
			{
				ExpressCompany = TakeExpress.ExpressCompany,
				TrackingCode = TakeExpress.TrackingCode,
				ConsigneePhone = TakeExpress.ConsigneePhone,
				Consignee = TakeExpress.Consignee,
				PickCode = TakeExpress.PickCode,
				YiZhanName = TakeExpress.YiZhanName,
				Weight = TakeExpress.Weight,
				ReceiveAddress1 = TakeExpress.ReceiveAddress1,
				ReceiveAddress2 = TakeExpress.ReceiveAddress2,
				ReceivePhoneNumber = TakeExpress.ReceivePhoneNumber,
				MissionName = "【快递】 【"+TakeExpress.ExpressCompany+"】",
				TypeId = "00000000-0000-0000-0000-000000000001",
				PostUserId = user.Id,
				PostTime = time,
				InvalidTime = time.AddDays(2.0),
				MissionNotes = TakeExpress.MissionNotes,
				MissionReward = TakeExpress.MissionReward
			};
			var result =_takeExpress.Post(post);
			if (result==1)
			{
				//success
			}
			return Page();
		}
	}
}
