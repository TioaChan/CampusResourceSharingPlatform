using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Model.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampusResourceSharingPlatform.Web.Areas.Identity.Pages.Account.Manage
{
	public class StudentIdentityModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public StudentIdentityModel(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public bool IsStudentIdentityConfirmed { get; set; }

		[Required(ErrorMessage = "你还没有填写身份证号码")]
		[MaxLength(18,ErrorMessage = "身份证号码长度为18位")]
		[Display(Name = "身份证号码")]
		public string PrcId { get; set; }

		[Required(ErrorMessage = "你还没有填写学号")]
		[MaxLength(12, ErrorMessage = "学号的长度为12位")]
		[Display(Name = "学号")]
		public string SchoolId { get; set; }

		[Required(ErrorMessage = "你还没有填写姓名")]
		[Display(Name = "姓名")]
		public string RealName { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }
		public class InputModel
		{
			[Required(ErrorMessage = "你还没有填写身份证号码")]
			[MaxLength(18, ErrorMessage = "身份证号码长度为18位")]
			[Display(Name = "身份证号码")]
			public string NewPrcId { get; set; }

			[Required(ErrorMessage = "你还没有填写学号")]
			[MaxLength(12, ErrorMessage = "学号的长度为12位")]
			[Display(Name = "学号")]
			public string NewSchoolId { get; set; }

			public int SchoolCardNo { get; set; }
			[Required(ErrorMessage = "你还没有填写姓名")]
			[Display(Name = "姓名")]
			public string NewRealName { get; set; }
		}

		private async Task LoadAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			PrcId = user.IdCardNo;
			SchoolId = user.SchoolCardNo;
			RealName = user.RealName;

			Input = new InputModel
			{
				NewPrcId = PrcId,
				NewSchoolId = SchoolId,
				NewRealName = RealName,
			};
			IsStudentIdentityConfirmed = user.StudentIdentityConfirmed;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}
			await LoadAsync();
			return Page();
		}

		public async Task<IActionResult> OnPostVerifyStudentIdentityAsync()
		{
			if (!IsStudentIdentityConfirmed)
			{
				var user = await _userManager.GetUserAsync(User);
				if (user == null)
				{
					return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
				}

				if (!ModelState.IsValid)
				{
					await LoadAsync();
					return Page();
				}

				if (Input.NewPrcId != user.IdCardNo && Input.NewSchoolId != user.SchoolCardNo && Input.NewRealName != user.RealName)
				{
					user.IdCardNo = Input.NewPrcId;
					user.SchoolCardNo = Input.NewSchoolId;
					user.RealName = Input.NewRealName;
					user.StudentIdentityConfirmed = true;
					var result = await _userManager.UpdateAsync(user);
					if (!result.Succeeded)
					{
						StatusMessage = "Error：学生身份认证失败，请重新认证。";
					}
					StatusMessage = "Success：学生身份认证成功。";
				}
			}
			else
			{
				StatusMessage = "Error：你已经认证了学生身份，无需再次认证。";
			}
			await LoadAsync();
			return Page();
		}
	}
}
