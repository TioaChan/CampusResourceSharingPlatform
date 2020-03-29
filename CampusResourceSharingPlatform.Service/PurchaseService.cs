using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;

namespace CampusResourceSharingPlatform.Service
{
	public class PurchaseService:IPurchaseService<Purchase>
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
