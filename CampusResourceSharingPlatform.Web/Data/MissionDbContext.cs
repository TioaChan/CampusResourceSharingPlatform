using CampusResourceSharingPlatform.Web.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace CampusResourceSharingPlatform.Web.Data
{
	public class MissionDbContext:DbContext
	{
		public MissionDbContext(DbContextOptions<MissionDbContext> options):base(options)
		{
			
		}

		public DbSet<MissionDetail> MissionDetails { get; set; }

		public DbSet<MissionType> MissionTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<MissionDetail>()
				.HasOne(a => a.MissionType)
				.WithMany(b => b.MissionDetails)
				.HasForeignKey(c => c.TypeId);
		}
	}
}
