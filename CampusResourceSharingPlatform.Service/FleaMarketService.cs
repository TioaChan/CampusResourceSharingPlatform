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
	public class FleaMarketService : IFleaMarketService<SecondHand>
	{
		private readonly ApplicationDbContext _context;

		public FleaMarketService(ApplicationDbContext context)
		{
			_context = context;
		}

		public int Post(SecondHand newPost)
		{
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
			var post = await _context.MissionFleaMarket
				.Where(p => p.PostUserId == userId && p.DeletedMark == false)
				.OrderByDescending(p => p.PostTime)
				.FirstOrDefaultAsync();
			return post;
		}

		public async Task<List<SecondHand>> GetAllActiveMissionAsync()
		{
			var post = await _context.MissionFleaMarket
				.Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false)
				.Include(f => f.PostUser)
				.Include(f => f.AcceptUser)
				.OrderByDescending(p => p.PostTime)
				.ToListAsync();
			return post;
		}

		public async Task<List<SecondHand>> GetAllActiveMissionByPostUserAsync(ApplicationUser postUser)
		{
			var post = await _context.MissionFleaMarket
				.Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false && p.PostUser == postUser)
				.Include(p => p.PostUser)
				.Include(f => f.AcceptUser)
				.OrderByDescending(p => p.PostTime)
				.ToListAsync();
			return post;
		}

		public async Task<List<SecondHand>> GetAllInvalidMissionAsync()
		{
			var post = await _context.MissionFleaMarket
				.Where(p => p.InvalidTime < DateTime.UtcNow && p.DeletedMark == false)
				.OrderByDescending(p => p.PostTime)
				.ToListAsync();
			return post;
		}

		public async Task<List<SecondHand>> GetAllInvalidMissionByPostUserAsync(ApplicationUser postUser)
		{
			var post = await _context.MissionFleaMarket
				.Where(p => p.InvalidTime < DateTime.UtcNow && p.DeletedMark == false && p.PostUser == postUser)
				.OrderByDescending(p => p.PostTime)
				.ToListAsync();
			return post;
		}

		public async Task<List<SecondHand>> GetAllDeletedMissionAsync()
		{
			var post = await _context.MissionFleaMarket
				.Where(p => p.DeletedMark == true)
				.OrderByDescending(p => p.PostTime)
				.ToListAsync();
			return post;
		}

		public async Task<List<SecondHand>> GetAllDeletedMissionByPostUserAsync(ApplicationUser postUser)
		{
			var post = await _context.MissionFleaMarket
				.Where(p => p.DeletedMark == true && p.PostUser == postUser)
				.OrderByDescending(p => p.PostTime)
				.ToListAsync();
			return post;
		}

		public async Task<List<SecondHand>> GetTop10ActiveMissionAsync()
		{
			var post = await _context.MissionFleaMarket
				.Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false)
				.OrderByDescending(p => p.PostTime)
				.Take(10)
				.ToListAsync();
			return post;
		}

		public async Task<SecondHand> GetMissionById(string postId)
		{
			var post = await _context.MissionFleaMarket.FindAsync(postId);
			return post;
		}

		public int Update(SecondHand newPost)
		{
			_context.MissionFleaMarket.Update(newPost);
			_context.SaveChanges();
			return 1;
		}

		public async Task<SecondHand> GetActiveMissionById(string postId)
		{
			var post = await _context.MissionFleaMarket
				.Where(p => p.DeletedMark == false && p.Id == postId)
				.FirstOrDefaultAsync();
			return post;
		}
	}
}
