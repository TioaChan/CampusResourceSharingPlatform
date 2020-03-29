using System.Linq;
using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CampusResourceSharingPlatform.Data
{
	public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions options):base(options)
		{
			
		}

		public DbSet<MissionType> MissionTypes { get; set; }

		public DbSet<Express> MissionExpresses { get; set; }

		public DbSet<License> ThirdLicenses { get; set; }

		public DbSet<Purchase> MissionPurchase { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}
			modelBuilder.Entity<MissionType>().Property(p => p.DeletedMark).HasDefaultValue(0);
			modelBuilder.Entity<ApplicationUser>().Property(p=>p.DeletedMark).HasDefaultValue(0);

			modelBuilder.Entity<Express>().Property(p=> p.IsAccepted).HasDefaultValue(0);
			modelBuilder.Entity<Express>().Property(p => p.IsCompleted).HasDefaultValue(0);
			modelBuilder.Entity<Express>().Property(p => p.DeletedMark).HasDefaultValue(0);

		}
	}
}
