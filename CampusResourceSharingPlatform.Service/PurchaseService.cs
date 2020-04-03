using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
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
			var post = await _context.MissionPurchase.Where(p => p.PostUserId == userId && p.DeletedMark == false).OrderByDescending(p => p.PostTime).FirstOrDefaultAsync();
			return post;
		}

		public async Task<List<Purchase>> GetAllActiveMissionAsync()
		{
			var post = await _context.MissionPurchase.Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Purchase>> GetAllActiveMissionAsync(ApplicationUser user)
		{
			var post = await _context.MissionPurchase.Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false && p.PostUser == user).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Purchase>> GetAllInvalidMissionAsync()
		{
			var post = await _context.MissionPurchase.Where(p => p.InvalidTime < DateTime.UtcNow && p.DeletedMark == false).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Purchase>> GetAllInvalidMissionAsync(ApplicationUser user)
		{
			var post = await _context.MissionPurchase.Where(p => p.InvalidTime < DateTime.UtcNow && p.DeletedMark == false && p.PostUser == user).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Purchase>> GetAllDeletedMissionAsync()
		{
			var post = await _context.MissionPurchase.Where(p => p.DeletedMark == true).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Purchase>> GetAllDeletedMissionAsync(ApplicationUser user)
		{
			var post = await _context.MissionPurchase.Where(p => p.DeletedMark == true && p.PostUser == user).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Purchase>> GetTop10ActiveMissionAsync()
		{
			var post = await _context.MissionPurchase.Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false).OrderByDescending(p => p.PostTime).Take(10).ToListAsync();
			return post;
		}

		public async Task<Purchase> GetMissionById(string postId)
		{
			var post = await _context.MissionPurchase.FindAsync(postId);
			return post;
		}

		public int Update(Purchase newPost)
		{
			_context.MissionPurchase.Update(newPost);
			_context.SaveChanges();
			return 1;
		}

		public async Task<Purchase> GetActiveMissionById(string postId)
		{
			var post = await _context.MissionPurchase.Where(p => p.DeletedMark == false && p.Id == postId)
				.FirstOrDefaultAsync();
			return post;
		}
	}
}
