﻿using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.EntityFrameworkCore;
using System;
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
			newPost.Id = Guid.NewGuid().ToString();
			var result = _context.MissionHire.AddAsync(newPost);
			if (!result.IsCompletedSuccessfully) return 0;
			_context.SaveChanges();
			return 1;
		}

		public Task<int> PostAsync(Hire newPost)
		{
			throw new NotImplementedException();
		}

		public async Task<Hire> GetLastMissionInfoAsync(string userId)
		{
			var post = await _context.MissionHire.OrderByDescending(p => p.PostTime)
				.Where(p => p.PostUserId == userId).FirstOrDefaultAsync();
			return post;
		}
	}
}
