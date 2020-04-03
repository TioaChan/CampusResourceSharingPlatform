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
	public class HireService : IHireService<Hire>
	{
		private readonly ApplicationDbContext _context;

		public HireService(ApplicationDbContext context)
		{
			_context = context;
		}

		public int Post(Hire newPost)
		{
			var result = _context.MissionHire.AddAsync(newPost);
			if (!result.IsCompletedSuccessfully) return 0;
			_context.SaveChanges();
			return 1;
		}

		public Task<int> PostAsync(Hire newPost)
		{
			throw new NotImplementedException();
		}

		public int Update(Hire newPost)
		{
			_context.MissionHire.Update(newPost);
			_context.SaveChanges();
			return 1;
		}

		public async Task<Hire> GetLastMissionInfoAsync(string userId)
		{
			var post = await _context.MissionHire.Where(p => p.PostUserId == userId && p.DeletedMark == false).OrderByDescending(p => p.PostTime).FirstOrDefaultAsync();
			return post;
		}

		public async Task<List<Hire>> GetAllActiveMissionAsync()
		{
			var post = await _context.MissionHire.Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Hire>> GetAllActiveMissionByPostUserAsync(ApplicationUser postUser)
		{
			var post = await _context.MissionHire.Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false && p.PostUser == postUser).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Hire>> GetAllInvalidMissionAsync()
		{
			var post = await _context.MissionHire.Where(p => p.InvalidTime < DateTime.UtcNow && p.DeletedMark == false).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Hire>> GetAllInvalidMissionByPostUserAsync(ApplicationUser postUser)
		{
			var post = await _context.MissionHire.Where(p => p.InvalidTime < DateTime.UtcNow && p.DeletedMark == false && p.PostUser == postUser).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Hire>> GetAllDeletedMissionAsync()
		{
			var post = await _context.MissionHire.Where(p => p.DeletedMark == true).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Hire>> GetAllDeletedMissionByPostUserAsync(ApplicationUser postUser)
		{
			var post = await _context.MissionHire.Where(p => p.DeletedMark == true && p.PostUser == postUser).OrderByDescending(p => p.PostTime).ToListAsync();
			return post;
		}

		public async Task<List<Hire>> GetTop10ActiveMissionAsync()
		{
			var post = await _context.MissionHire.Where(p => p.InvalidTime > DateTime.UtcNow && p.DeletedMark == false).OrderByDescending(p => p.PostTime).Take(10).ToListAsync();
			return post;
		}

		public async Task<Hire> GetMissionById(string postId)
		{
			var post = await _context.MissionHire.FindAsync(postId);
			return post;
		}

		public async Task<Hire> GetActiveMissionById(string postId)
		{
			var post = await _context.MissionHire.Where(p => p.DeletedMark == false && p.Id == postId)
				.FirstOrDefaultAsync();
			return post;
		}
	}
}
