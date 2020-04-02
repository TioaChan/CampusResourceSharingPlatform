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
	public class TakeExpressService : ITakeExpressService<Express>
	{
		private readonly ApplicationDbContext _context;

		public TakeExpressService(ApplicationDbContext context)
		{
			_context = context;
		}
		public int Post(Express newPost)
		{
			var result = _context.MissionExpresses.AddAsync(newPost);
			if (!result.IsCompletedSuccessfully) return 0;
			_context.SaveChanges();
			return 1;
		}

		public Task<int> PostAsync(Express newPost)
		{
			throw new NotImplementedException();
		}

		public int Update(Express newPost)
		{
			_context.MissionExpresses.Update(newPost);
			_context.SaveChanges();
			return 1;
		}

		public async Task<Express> GetLastMissionInfoAsync(string userId)
		{
			var post = await _context.MissionExpresses.Where(p => p.PostUserId == userId && p.DeletedMark == false).OrderByDescending(p => p.PostTime).FirstOrDefaultAsync();
			return post;
		}

		public async Task<List<Express>> GetAllActiveMissionAsync()
		{
			var post = await _context.MissionExpresses.OrderByDescending(p => p.PostTime).Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false).ToListAsync();
			return post;
		}

		public async Task<List<Express>> GetTop10ActiveMissionAsync()
		{
			var post = await _context.MissionExpresses.OrderByDescending(p => p.PostTime).Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark==false).Take(10).ToListAsync();
			return post;
		}

		public async Task<Express> GetMissionById(string postId)
		{
			var post = await _context.MissionExpresses.FindAsync(postId);
			return post;
		}

		public async Task<Express> GetActiveMissionById(string postId)
		{
			var post = await _context.MissionExpresses.Where(p => p.DeletedMark == false && p.Id == postId)
				.FirstOrDefaultAsync();
			return post;
		}
	}
}
