using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Post
{
	[Authorize(Roles = "Administrators")]
	public class IndexModel : PageModel
	{
		private readonly IMissionService<Express> _takeExpressService;
		private readonly IMissionService<Purchase> _purchaseService;
		private readonly IMissionService<SecondHand> _fleaMarketService;
		private readonly IMissionService<Hire> _hireService;
		private readonly UserManager<ApplicationUser> _userManager;

		public IndexModel(IMissionService<Express> takeExpressService,
			IMissionService<Purchase> purchaseService,
			IMissionService<SecondHand> fleaMarketService,
			IMissionService<Hire> hireService,
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
					ActivePostCount = _takeExpressService.GetAllActiveMissionAsync().Result.Count.ToString(),
					InvalidPostCount = _takeExpressService.GetAllInvalidMissionAsync().Result.Count.ToString(),
					DeletedPostCount = _takeExpressService.GetAllDeletedMissionAsync().Result.Count.ToString()
				},
				Purchase = new PurchasePostCount
				{
					ActivePostCount = _purchaseService.GetAllActiveMissionAsync().Result.Count.ToString(),
					InvalidPostCount = _purchaseService.GetAllInvalidMissionAsync().Result.Count.ToString(),
					DeletedPostCount = _purchaseService.GetAllDeletedMissionAsync().Result.Count.ToString()
				},
				FleaMarket = new FleaMarketPostCount
				{
					ActivePostCount = _fleaMarketService.GetAllActiveMissionAsync().Result.Count.ToString(),
					InvalidPostCount = _fleaMarketService.GetAllInvalidMissionAsync().Result.Count.ToString(),
					DeletedPostCount = _fleaMarketService.GetAllDeletedMissionAsync().Result.Count.ToString()
				},
				HirePost = new HirePostCount
				{
					ActivePostCount = _hireService.GetAllActiveMissionAsync().Result.Count.ToString(),
					InvalidPostCount = _hireService.GetAllInvalidMissionAsync().Result.Count.ToString(),
					DeletedPostCount = _hireService.GetAllDeletedMissionAsync().Result.Count.ToString()
				},
			};
			return Page();
		}
	}
}
