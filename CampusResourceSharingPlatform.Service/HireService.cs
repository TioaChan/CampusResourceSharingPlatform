using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;

namespace CampusResourceSharingPlatform.Service
{
	public class HireService:IHireService<Hire>
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
	}
}
