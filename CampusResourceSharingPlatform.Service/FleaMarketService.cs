using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Service
{
	public class FleaMarketService : IFleaMarketService<SecondHand>
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

		public async Task<SecondHand> GetLastMissionInfoAsync(string userId)
		{
			var post = await _context.MissionFleaMarket.OrderByDescending(p => p.PostTime)
				.Where(p => p.PostUserId == userId).FirstOrDefaultAsync();
			return post;
		}
	}
}
