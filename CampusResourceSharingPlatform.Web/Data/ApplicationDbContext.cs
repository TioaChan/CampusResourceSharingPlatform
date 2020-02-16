using System.Linq;
using CampusResourceSharingPlatform.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CampusResourceSharingPlatform.Web.Data
{
	public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions options):base(options)
		{
			
		}
		public DbSet<MissionDetail> MissionDetails { get; set; }

		public DbSet<MissionType> MissionTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

			modelBuilder.Entity<MissionDetail>().Property(p => p.IsAccepted).HasDefaultValue(0);
			modelBuilder.Entity<MissionDetail>().Property(p => p.IsCompleted).HasDefaultValue(0);
			modelBuilder.Entity<MissionDetail>().Property(p => p.DeletedMark).HasDefaultValue(0);
			modelBuilder.Entity<MissionType>().Property(p => p.DeletedMark).HasDefaultValue(0);
		}
	}
}
