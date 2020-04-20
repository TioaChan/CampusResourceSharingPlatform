using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.i.Pages
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ITakeExpressService<Express> _takeExpressService;
		private readonly IPurchaseService<Purchase> _purchaseService;
		private readonly IMissionService<SecondHand> _fleaMarketService;
		private readonly IHireService<Hire> _hireService;
		private readonly UserManager<ApplicationUser> _userManager;

		public IndexModel(ITakeExpressService<Express> takeExpressService,
			IPurchaseService<Purchase> purchaseService,
			IMissionService<SecondHand> fleaMarketService,
			IHireService<Hire> hireService,
			UserManager<ApplicationUser> userManager)
		{
			_takeExpressService = takeExpressService;
			_purchaseService = purchaseService;
			_fleaMarketService = fleaMarketService;
			_hireService = hireService;
			_userManager = userManager;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public class TakeExpressPostCount
		{
			public string ActivePostCount { get; set; }
			public string InvalidPostCount { get; set; }
			public string DeletedPostCount { get; set; }
		}
		public class PurchasePostCount
		{
			public string ActivePostCount { get; set; }
			public string InvalidPostCount { get; set; }
			public string DeletedPostCount { get; set; }
		}
		public class FleaMarketPostCount
		{
			public string ActivePostCount { get; set; }
			public string InvalidPostCount { get; set; }
			public string DeletedPostCount { get; set; }
		}
		public class HirePostCount
		{
			public string ActivePostCount { get; set; }
			public string InvalidPostCount { get; set; }
			public string DeletedPostCount { get; set; }
		}
		public class PostCount
		{
			public TakeExpressPostCount TakeExpress { get; set; }

			public PurchasePostCount Purchase { get; set; }

			public FleaMarketPostCount FleaMarket { get; set; }

			public HirePostCount HirePost { get; set; }
		}

		public PostCount Count { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			Count = new PostCount
			{
				TakeExpress = new TakeExpressPostCount
				{
					ActivePostCount = _takeExpressService.GetAllActiveMissionByPostUserAsync(user).Result.Count.ToString(),
					InvalidPostCount = _takeExpressService.GetAllInvalidMissionByPostUserAsync(user).Result.Count.ToString(),
					DeletedPostCount = _takeExpressService.GetAllDeletedMissionByPostUserAsync(user).Result.Count.ToString()
				},
				Purchase = new PurchasePostCount
				{
					ActivePostCount = _purchaseService.GetAllActiveMissionByPostUserAsync(user).Result.Count.ToString(),
					InvalidPostCount = _purchaseService.GetAllInvalidMissionByPostUserAsync(user).Result.Count.ToString(),
					DeletedPostCount = _purchaseService.GetAllDeletedMissionByPostUserAsync(user).Result.Count.ToString()
				},
				FleaMarket = new FleaMarketPostCount
				{
					ActivePostCount = _fleaMarketService.GetAllActiveMissionByPostUserAsync(user).Result.Count.ToString(),
					InvalidPostCount = _fleaMarketService.GetAllInvalidMissionByPostUserAsync(user).Result.Count.ToString(),
					DeletedPostCount = _fleaMarketService.GetAllDeletedMissionByPostUserAsync(user).Result.Count.ToString()
				},
				HirePost = new HirePostCount
				{
					ActivePostCount = _hireService.GetAllActiveMissionByPostUserAsync(user).Result.Count.ToString(),
					InvalidPostCount = _hireService.GetAllInvalidMissionByPostUserAsync(user).Result.Count.ToString(),
					DeletedPostCount = _hireService.GetAllDeletedMissionByPostUserAsync(user).Result.Count.ToString()
				},
			};
			return Page();
		}
	}
}
