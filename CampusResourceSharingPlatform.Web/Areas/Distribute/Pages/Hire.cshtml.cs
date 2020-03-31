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
	public class HireModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IWebHostEnvironment _iWebHostEnvironment;
		private readonly IHireService<Hire> _hire;

		public HireModel(UserManager<ApplicationUser> userManager,
			IWebHostEnvironment iWebHostEnvironment,
			IHireService<Hire> hire)
		{
			_userManager = userManager;
			_iWebHostEnvironment = iWebHostEnvironment;
			_hire = hire;
		}

		[BindProperty]
		public HireInputModel HireInput { get; set; }

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


		public class HireInputModel
		{
			/// <summary>
			/// 物品照片url
			/// </summary>
			[Display(Name = "图片")]
			public IFormFile GoodsPhoto { get; set; }

			/// <summary>
			/// 物品名称
			/// </summary>
			[Required(ErrorMessage = "物品名称 为必填项")]
			[Display(Name = "物品名称")]
			public string GoodsName { get; set; }

			/// <summary>
			/// 物品定价 索赔用
			/// </summary>
			[Required(ErrorMessage = "物品定价 为必填项")]
			[Display(Name = "物品定价")]
			public double GoodsPrice { get; set; }

			/// <summary>
			/// 物品描述
			/// </summary>
			[Required(ErrorMessage = "描述 为必填项")]
			[Display(Name = "描述")]
			public string GoodsDescription { get; set; }

			/// <summary>
			/// 分类
			/// </summary>
			public string GoodsCategory { get; set; }

			/// <summary>
			/// 租金
			/// </summary>
			[Required(ErrorMessage = "租金 为必填项")]
			[Display(Name = "设置租金")]
			public double GoodsRent { get; set; }

			/// <summary>
			/// 时限
			/// </summary>
			[Required(ErrorMessage = "租借期限 为必填项")]
			[Display(Name = "租借期限")]
			public double TimeLimit { get; set; }

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

		}
		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null || !user.StudentIdentityConfirmed) return RedirectToPage("Index");
			var post = await _hire.GetLastMissionInfoAsync(user.Id);
			HireInput = new HireInputModel
			{
				PostUserId = user.Id
			};
			if (post != null)
			{
				HireInput.PosterAddress1 = post.PosterAddress1;
				HireInput.PosterAddress2 = post.PosterAddress2;
				HireInput.PosterPhoneNumber = post.PosterPhoneNumber;
			}
			PostUserId = user.Id;
			EditMark = false;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null) return RedirectToPage("Index");
			if (user.Id != PostUserId) return RedirectToPage("Index");
			if (HireInput.GoodsPhoto==null)
			{
				PostUserId = user.Id;
				ModelState.AddModelError("", "请选择一张图片");
				return Page();
			}
			if (HireInput.GoodsPhoto.Length == 0)
			{
				PostUserId = user.Id;
				ModelState.AddModelError("", "请选择一张图片");
				return Page();
			}
			var uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images", "distribute");
			var uploadFileName = Guid.NewGuid() + Path.GetExtension(HireInput.GoodsPhoto.FileName);
			var filePath = Path.Combine(uploadFolder, uploadFileName);
			if (!Directory.Exists(uploadFolder))
			{
				Directory.CreateDirectory(uploadFolder);
			}
			await HireInput.GoodsPhoto.CopyToAsync(new FileStream(filePath, FileMode.Create));
			var time = DateTime.UtcNow;
			var post = new Hire
			{
				Id = Guid.NewGuid().ToString(),
				GoodsPhotoUrl = "/images/distribute/" + uploadFileName,
				GoodsName = HireInput.GoodsName,
				GoodsPrice = HireInput.GoodsPrice,
				GoodsDescription = HireInput.GoodsDescription,
				GoodsCategory = "未实现",
				GoodsRent = HireInput.GoodsRent,
				TimeLimit = HireInput.TimeLimit,
				MissionName = "【物品租借】【" + HireInput.GoodsName + "】",
				TypeId = "00000000-0000-0000-0000-000000000004",
				PostUserId = user.Id,
				PostTime = time,
				InvalidTime = time.AddDays(2.0),
				ExpiredTime = time.AddDays(HireInput.TimeLimit),
				MissionNotes = HireInput.MissionNotes,
				PosterAddress1 = HireInput.PosterAddress1,
				PosterAddress2 = HireInput.PosterAddress2,
				PosterPhoneNumber = HireInput.PosterPhoneNumber,
			};
			var result = _hire.Post(post);
			if (result == 1)
			{
				StatusMessage = "Success:发布成功";
				return RedirectToPage("/Hire", new { Area = "Posts", postId = post.Id });
			}
			StatusMessage = "Error:发布失败";
			return RedirectToPage();
		}

		public async Task<IActionResult> OnGetEditMissionAsync(string postId)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null || !user.StudentIdentityConfirmed) return RedirectToPage("Index");
			var post = await _hire.GetMissionById(postId);
			if (post.PostUserId!=user.Id) return RedirectToPage("Index");
			HireInput = new HireInputModel
			{
				PostUserId = user.Id,
				GoodsName = post.GoodsName,
				GoodsCategory = "未实现",
				GoodsPrice = post.GoodsPrice,
				GoodsDescription = post.GoodsDescription,
				GoodsRent = post.GoodsRent,
				TimeLimit = post.TimeLimit,
				MissionNotes = post.MissionNotes,
				PosterAddress1 = post.PosterAddress1,
				PosterAddress2 = post.PosterAddress2,
				PosterPhoneNumber = post.PosterPhoneNumber,
			};
			PostId = post.Id;
			GoodsUrl = post.GoodsPhotoUrl;
			PostUserId = user.Id;
			EditMark = true;
			return Page();
		}

		public async Task<IActionResult> OnPostEditMissionAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null || user.Id != PostUserId || PostId==null) return RedirectToPage("Index");
			var post = new Hire();
			if (HireInput.GoodsPhoto != null && HireInput.GoodsPhoto.Length!=0)
			{
				var uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images", "distribute");
				var uploadFileName = Guid.NewGuid() + Path.GetExtension(HireInput.GoodsPhoto.FileName);
				var filePath = Path.Combine(uploadFolder, uploadFileName);
				if (!Directory.Exists(uploadFolder))
				{
					Directory.CreateDirectory(uploadFolder);
				}
				await HireInput.GoodsPhoto.CopyToAsync(new FileStream(filePath, FileMode.Create));
				post.GoodsPhotoUrl = "/images/distribute/" + uploadFileName;
			}
			else
			{
				post.GoodsPhotoUrl = GoodsUrl;
			}
			var time = DateTime.UtcNow;
			post.Id = PostId;
			post.GoodsName = HireInput.GoodsName;
			post.GoodsPrice = HireInput.GoodsPrice;
			post.GoodsDescription = HireInput.GoodsDescription;
			post.GoodsCategory = "未实现";
			post.GoodsRent = HireInput.GoodsRent;
			post.TimeLimit = HireInput.TimeLimit;
			post.MissionName = "【物品租借】【" + HireInput.GoodsName + "】";
			post.TypeId = "00000000-0000-0000-0000-000000000004";
			post.PostUserId = user.Id;
			post.PostTime = time;
			post.InvalidTime = time.AddDays(2.0);
			post.MissionNotes = HireInput.MissionNotes;
			post.PosterAddress1 = HireInput.PosterAddress1;
			post.PosterAddress2 = HireInput.PosterAddress2;
			post.PosterPhoneNumber = HireInput.PosterPhoneNumber;
			post.ExpiredTime = time.AddDays(HireInput.TimeLimit);
			var result = _hire.Update(post);
			if (result == 1)
			{
				StatusMessage = "Success:修改成功";
				return RedirectToPage("/Hire", new { Area = "Posts", postId = post.Id });
			}
			StatusMessage = "Success:修改失败";
			return Page();
		}
	}
}
