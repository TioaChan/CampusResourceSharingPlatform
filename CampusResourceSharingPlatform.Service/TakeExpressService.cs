using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

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
			var post = await _context.MissionExpresses.Include(f => f.ExpressCompany).Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Express>> GetAllActiveMissionByPostUserAsync(ApplicationUser postUser)
		{
			var post = await _context.MissionExpresses.Include(f => f.ExpressCompany).Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false && p.PostUser == postUser).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Express>> GetAllInvalidMissionAsync()
		{
			var post = await _context.MissionExpresses.Include(f => f.ExpressCompany).Where(p => p.InvalidTime < DateTime.UtcNow && p.DeletedMark == false).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Express>> GetAllInvalidMissionByPostUserAsync(ApplicationUser postUser)
		{
			var post = await _context.MissionExpresses.Include(f => f.ExpressCompany).Where(p => p.InvalidTime < DateTime.UtcNow && p.DeletedMark == false && p.PostUser == postUser).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Express>> GetAllDeletedMissionAsync()
		{
			var post = await _context.MissionExpresses.Include(f => f.ExpressCompany).Where(p => p.DeletedMark == true).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Express>> GetAllDeletedMissionByPostUserAsync(ApplicationUser postUser)
		{
			var post = await _context.MissionExpresses.Include(f => f.ExpressCompany).Where(p => p.DeletedMark == true && p.PostUser == postUser).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Express>> GetTop10ActiveMissionAsync()
		{
			var post = await _context.MissionExpresses.Include(f => f.ExpressCompany).Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false).OrderByDescending(p => p.PostTime).Take(10).ToListAsync();
			return post;
		}

		public async Task<Express> GetMissionById(string postId)
		{
			var post = await _context.MissionExpresses.Include(f=>f.ExpressCompany).Where(p => p.Id == postId).FirstOrDefaultAsync();
			return post;
		}

		public async Task<Express> GetActiveMissionById(string postId)
		{
			var post = await _context.MissionExpresses.Include(f => f.ExpressCompany).Where(p => p.DeletedMark == false && p.Id == postId)
				.FirstOrDefaultAsync();
			return post;
		}
	}
}
