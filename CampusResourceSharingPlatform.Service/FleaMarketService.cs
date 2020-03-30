using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;

namespace CampusResourceSharingPlatform.Service
{
	public class FleaMarketService: IFleaMarketService<SecondHand>
	{
		private readonly ApplicationDbContext _context;

		public FleaMarketService(ApplicationDbContext context)
		{
			_context = context;
		}

		public int Post(SecondHand newPost)
		{
			newPost.Id = Guid.NewGuid().ToString();
			var result = _context.MissionFleaMarket.AddAsync(newPost);
			if (!result.IsCompletedSuccessfully) return 0;
			_context.SaveChanges();
			return 1;
		}

		public Task<int> PostAsync(SecondHand newPost)
		{
			throw new NotImplementedException();
		}
	}
}
