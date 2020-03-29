using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;
using System;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Service
{
	public class PurchaseService : IPurchaseService<Purchase>
	{
		private readonly ApplicationDbContext _context;

		public PurchaseService(ApplicationDbContext context)
		{
			_context = context;
		}

		public int Post(Purchase newPost)
		{
			newPost.Id = Guid.NewGuid().ToString();
			var result = _context.MissionPurchase.AddAsync(newPost);
			if (!result.IsCompletedSuccessfully) return 0;
			_context.SaveChanges();
			return 1;
		}

		public Task<int> PostAsync(Purchase newPost)
		{
			throw new NotImplementedException();
		}
	}
}
