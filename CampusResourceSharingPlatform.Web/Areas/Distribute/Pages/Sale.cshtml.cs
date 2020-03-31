using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.Distribute.Pages
{
	public class SaleModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IWebHostEnvironment _iWebHostEnvironment;
		private readonly IFleaMarketService<SecondHand> _fleaMarket;

		public SaleModel(UserManager<ApplicationUser> userManager,
			IWebHostEnvironment iWebHostEnvironment,
			IFleaMarketService<SecondHand> fleaMarket)
		{
			_userManager = userManager;
			_iWebHostEnvironment = iWebHostEnvironment;
			_fleaMarket = fleaMarket;
		}

		[BindProperty]
		public SaleInputModel SaleInput { get; set; }

		[BindProperty]
		public string PostUserId { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

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

			[Display(Name = "图片")]
			[Required(ErrorMessage = "请选择一张照片")]
			public IFormFile GoodsPhoto { get; set; }

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
			if (user == null || !user.StudentIdentityConfirmed) return RedirectToPage("Index");
			var post = await _fleaMarket.GetLastMissionInfoAsync(user.Id);
			SaleInput = new SaleInputModel
			{
				PostUserId = user.Id
			};
			if (post != null)
			{
				SaleInput.PosterAddress1 = post.PosterAddress1;
				SaleInput.PosterAddress2 = post.PosterAddress2;
				SaleInput.PosterPhoneNumber = post.PosterPhoneNumber;
			}
			PostUserId = user.Id;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null) return RedirectToPage("Index");
			if (user.Id != PostUserId) return RedirectToPage("Index");
			if (SaleInput.GoodsPhoto.Length == 0) return Page();
			var uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images", "distribute");
			var uploadFileName = Guid.NewGuid() + Path.GetExtension(SaleInput.GoodsPhoto.FileName);
			var filePath = Path.Combine(uploadFolder, uploadFileName);
			if (!Directory.Exists(uploadFolder))
			{
				Directory.CreateDirectory(uploadFolder);
			}
			await SaleInput.GoodsPhoto.CopyToAsync(new FileStream(filePath, FileMode.Create));
			var time = DateTime.UtcNow;
			var post = new SecondHand
			{
				Id = Guid.NewGuid().ToString(),
				GoodsPhotoUrl = "/images/distribute/" + uploadFileName,
				GoodsName = SaleInput.GoodsName,
				GoodsPrice = SaleInput.GoodsPrice,
				GoodsQuality = SaleInput.GoodsQuality,
				GoodsDescription = SaleInput.GoodsDescription,
				MissionName = "【二手出售】【" + SaleInput.GoodsName + "】",
				TypeId = "00000000-0000-0000-0000-000000000003",
				PostUserId = user.Id,
				PostTime = time,
				InvalidTime = time.AddDays(2.0),
				MissionNotes = SaleInput.MissionNotes,
				PosterAddress1 = SaleInput.PosterAddress1,
				PosterAddress2 = SaleInput.PosterAddress2,
				PosterPhoneNumber = SaleInput.PosterPhoneNumber,
			};
			var result = _fleaMarket.Post(post);
			if (result == 1)
			{
				StatusMessage = "Success:发布成功";
				return RedirectToPage("/Sale", new { Area = "Posts", postId = post.Id });
			}
			StatusMessage = "Error:发布失败";
			return RedirectToPage();
		}
	}
}
