using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<Purchase> GetLastMissionInfoAsync(string userId)
		{
			var post = await _context.MissionPurchase.OrderByDescending(p => p.PostTime)
				.Where(p => p.PostUserId == userId).FirstOrDefaultAsync();
			return post;
		}

		public async Task<List<Purchase>> GetAllActiveMissionAsync()
		{
			var post = await _context.MissionPurchase.OrderByDescending(p => p.PostTime).Where(p => p.InvalidTime > DateTime.UtcNow).ToListAsync();
			return post;
		}

		public async Task<List<Purchase>> GetTop10ActiveMissionAsync()
		{
			var post = await _context.MissionPurchase.OrderByDescending(p => p.PostTime).Where(p => p.InvalidTime > DateTime.UtcNow).Take(10).ToListAsync();
			return post;
		}
	}
}
