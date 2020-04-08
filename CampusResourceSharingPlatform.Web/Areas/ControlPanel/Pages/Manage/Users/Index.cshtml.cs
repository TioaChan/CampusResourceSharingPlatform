using CampusResourceSharingPlatform.Model.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Users
{
	[Authorize(Roles = "Administrators")]
	public class IndexModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ITakeExpressService<Express> _takeExpressService;
		private readonly IPurchaseService<Purchase> _purchaseService;
		private readonly IFleaMarketService<SecondHand> _fleaMarketService;
		private readonly IHireService<Hire> _hireService;

		public IEnumerable<ApplicationUser> Users { get; set; }

		public List<UserInformations> UserPackage { get; set; }
		public class UserInformations
		{
			public ApplicationUser User { get; set; }
			public int PostCount { get; set; }

			public int ExpressPostCount { get; set; }

			public int PurchasePostCount { get; set; }

			public int FleaMarketPostCount { get; set; }

			public int HireMarketPostCount { get; set; }
		}

		[TempData]
		public string StatusMessage { get; set; }

		public IndexModel(UserManager<ApplicationUser> userManager, 
			ITakeExpressService<Express> takeExpressService,
			IPurchaseService<Purchase> purchaseService,
			IFleaMarketService<SecondHand> fleaMarketService,
			IHireService<Hire> hireService)
		{
			_userManager = userManager;
			_takeExpressService = takeExpressService;
			_purchaseService = purchaseService;
			_fleaMarketService = fleaMarketService;
			_hireService = hireService;
		}

		private async Task LoadUsersAsync()
		{
			Users = await _userManager.Users.OrderBy(p=>p.DeletedMark).ToListAsync();
			UserPackage = new List<UserInformations>();
			foreach (var user in Users)
			{
				var userInformation = new UserInformations
				{
					User = user,
					ExpressPostCount = (await _takeExpressService.GetAllActiveMissionByPostUserAsync(user)).Count,
					PurchasePostCount = (await _purchaseService.GetAllActiveMissionByPostUserAsync(user)).Count,
					FleaMarketPostCount = (await _fleaMarketService.GetAllActiveMissionByPostUserAsync(user)).Count,
					HireMarketPostCount = (await _hireService.GetAllActiveMissionByPostUserAsync(user)).Count,
				};	
				UserPackage.Add(userInformation);
			}
		}

		public async Task<IActionResult> OnGetAsync()
		{
			await LoadUsersAsync();
			return Page();
		}
		public async Task<IActionResult> OnPostDeleteUserAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			user.DeletedMark = true;
			var result = await _userManager.UpdateAsync(user);
			StatusMessage = result.Succeeded ? "Success：用户删除成功。" : "Error：用户删除失败。";
			await LoadUsersAsync();
			return Page();
		}
		public async Task<IActionResult> OnPostRestoreUserAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			user.DeletedMark = false;
			var result = await _userManager.UpdateAsync(user);
			StatusMessage = result.Succeeded ? "Success：用户恢复成功。" : "Error：用户恢复失败。";
			await LoadUsersAsync();
			return Page();
		}
	}
}
