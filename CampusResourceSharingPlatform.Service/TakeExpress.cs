using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;

namespace CampusResourceSharingPlatform.Service
{
	public class TakeExpress:ITakeExpressService<Express>
	{
		private readonly ApplicationDbContext _dbContext;

		public TakeExpress(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public int Post(Express newPost)
		{
			newPost.Id = Guid.NewGuid().ToString();
			var result = _dbContext.MissionExpresses.AddAsync(newPost);
			if (!result.IsCompletedSuccessfully) return 0;
			_dbContext.SaveChanges();
			return 1;
		}

		public Task<int> PostAsync(Express newPost)
		{
			throw new NotImplementedException();
		}
	}
}
