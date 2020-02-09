using CampusResourceSharingPlatform.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusResourceSharingPlatform.Web.Data
{
	public class MissionDbContext:DbContext
	{
		public MissionDbContext(DbContextOptions<MissionDbContext> options):base(options)
		{
			
		}

		public DbSet<MissionData> MissionDatas { get; set; }

		public DbSet<MissionType> MissionTypes { get; set; }
	}
}
