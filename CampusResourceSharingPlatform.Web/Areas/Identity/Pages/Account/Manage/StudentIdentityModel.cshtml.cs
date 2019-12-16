using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Web.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

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

		[Required(ErrorMessage = "�㻹û����д���֤����")]
		[MaxLength(18,ErrorMessage = "���֤���볤��Ϊ18λ")]
		[Display(Name = "���֤����")]
		public string IdCardNo { get; set; }

		[Required(ErrorMessage = "�㻹û����дѧ��")]
		[MaxLength(12, ErrorMessage = "ѧ�ŵĳ���Ϊ12λ")]
		[Display(Name = "ѧ��")]
		public string SchoolCardNo { get; set; }

		[Required(ErrorMessage = "�㻹û����д����")]
		[Display(Name = "����")]
		public string RealName { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }
		public class InputModel
		{
			[Required(ErrorMessage = "�㻹û����д���֤����")]
			[MaxLength(18, ErrorMessage = "���֤���볤��Ϊ18λ")]
			[Display(Name = "���֤����")]
			public string NewIdCardNo { get; set; }

			[Required(ErrorMessage = "�㻹û����дѧ��")]
			[MaxLength(12, ErrorMessage = "ѧ�ŵĳ���Ϊ12λ")]
			[Display(Name = "ѧ��")]
			public string NewSchoolCardNo { get; set; }

			public int SchoolCardNo { get; set; }
			[Required(ErrorMessage = "�㻹û����д����")]
			[Display(Name = "����")]
			public string NewRealName { get; set; }
		}

		private async Task LoadAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			IdCardNo = user.IdCardNo;
			SchoolCardNo = user.SchoolCardNo;
			RealName = user.RealName;

			Input = new InputModel
			{
				NewIdCardNo = IdCardNo,
				NewSchoolCardNo = SchoolCardNo,
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

			if (Input.NewIdCardNo != user.IdCardNo && Input.NewSchoolCardNo!=user.SchoolCardNo && Input.NewRealName != user.RealName)
			{
				user.IdCardNo = Input.NewIdCardNo;
				user.SchoolCardNo = Input.NewSchoolCardNo;
				user.RealName = Input.NewRealName;
				user.StudentIdentityConfirmed = true;
				var result = await _userManager.UpdateAsync(user);
				if (!result.Succeeded)
				{
					await LoadAsync();
					return Page();
				}
				StatusMessage = "ERROR��ѧ�������֤ʧ�ܣ���������֤��";
				return RedirectToPage();
			}
			StatusMessage = "ѧ�������֤�ɹ�";
			return RedirectToPage();
		}
	}
}
