using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Model.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampusResourceSharingPlatform.Web.Areas.Distribute.Pages
{
	public class SaleModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public SaleModel(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		[BindProperty]
		public SaleInputModel SaleInput { get; set; }

		[BindProperty]
		public string PostUserId { get; set; }

		public class SaleInputModel
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
			public string PosterAddress1 { get; set; }

			/// <summary>
			/// 地址2
			/// </summary>
			[Display(Name = "联系地址2")]
			[Required(ErrorMessage = "联系地址2 为必填项")]
			public string PosterAddress2 { get; set; }

			[Display(Name = "联系方式")]
			[Required(ErrorMessage = "联系方式 为必填项")]
			public string PosterPhoneNumber { get; set; }

			/// <summary>
			/// 物品照片url
			/// </summary>
			[Required(ErrorMessage = "请选择一张照片")]
			[Display(Name = "上传照片")]
			public string GoodsPhotoUrl { get; set; }

			/// <summary>
			/// 物品名称
			/// </summary>
			[Required(ErrorMessage = "物品名称 为必填项")]
			[Display(Name = "物品名称")]
			public string GoodsName { get; set; }

			/// <summary>
			/// 物品定价
			/// </summary>
			[Required(ErrorMessage = "定价 为必填项")]
			[Display(Name = "定价")]
			public double GoodsPrice { get; set; }

			/// <summary>
			/// 物品质量
			/// </summary>
			[Required(ErrorMessage = "成色 为必填项")]
			[Display(Name = "成色")]
			public string GoodsQuality { get; set; }

			/// <summary>
			/// 物品描述
			/// </summary>
			[Required(ErrorMessage = "描述 为必填项")]
			[Display(Name = "描述")]
			public string GoodsDescription { get; set; }
		}
		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null) return RedirectToPage("Index");
			SaleInput = new SaleInputModel
			{
				PostUserId = user.Id,
			};
			PostUserId = user.Id;
			return Page();
		}
	}
}
