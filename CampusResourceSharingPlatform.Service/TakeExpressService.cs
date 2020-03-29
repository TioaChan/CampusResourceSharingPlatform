using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;
using System;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Service
{
	public class TakeExpressService : ITakeExpressService<Express>
	{
		private readonly ApplicationDbContext _dbContext;

		public TakeExpressService(ApplicationDbContext dbContext)
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
