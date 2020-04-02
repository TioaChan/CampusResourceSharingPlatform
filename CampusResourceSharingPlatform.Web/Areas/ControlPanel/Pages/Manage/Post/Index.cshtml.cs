using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Post
{
	[Authorize(Roles = "Administrators")]
	public class IndexModel : PageModel
	{
		private readonly ITakeExpressService<Express> _takeExpressService;
		private readonly IPurchaseService<Purchase> _purchaseService;
		private readonly IFleaMarketService<SecondHand> _fleaMarketService;
		private readonly IHireService<Hire> _hireService;

		public IndexModel(ITakeExpressService<Express> takeExpressService,
			IPurchaseService<Purchase> purchaseService,
			IFleaMarketService<SecondHand> fleaMarketService,
			IHireService<Hire> hireService)
		{
			_takeExpressService = takeExpressService;
			_purchaseService = purchaseService;
			_fleaMarketService = fleaMarketService;
			_hireService = hireService;
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

				},
				FleaMarket = new FleaMarketPostCount
				{

				},
				HirePost = new HirePostCount
				{

				},
			};
			return Page();
		}
	}
}
