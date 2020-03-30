using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CampusResourceSharingPlatform.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<MissionType> MissionTypes { get; set; }

		public DbSet<Express> MissionExpresses { get; set; }

		public DbSet<License> ThirdLicenses { get; set; }

		public DbSet<Purchase> MissionPurchase { get; set; }

		public DbSet<SecondHand> MissionFleaMarket { get; set; }

		public DbSet<Hire> MissionHire { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}
			modelBuilder.Entity<MissionType>().Property(p => p.DeletedMark).HasDefaultValue(0);
			modelBuilder.Entity<ApplicationUser>().Property(p => p.DeletedMark).HasDefaultValue(0);

			modelBuilder.Entity<Express>().Property(p => p.IsAccepted).HasDefaultValue(0);
			modelBuilder.Entity<Express>().Property(p => p.IsCompleted).HasDefaultValue(0);
			modelBuilder.Entity<Express>().Property(p => p.DeletedMark).HasDefaultValue(0);

			modelBuilder.Entity<Purchase>().Property(p => p.IsAccepted).HasDefaultValue(0);
			modelBuilder.Entity<Purchase>().Property(p => p.IsCompleted).HasDefaultValue(0);
			modelBuilder.Entity<Purchase>().Property(p => p.DeletedMark).HasDefaultValue(0);

			modelBuilder.Entity<SecondHand>().Property(p => p.IsAccepted).HasDefaultValue(0);
			modelBuilder.Entity<SecondHand>().Property(p => p.IsCompleted).HasDefaultValue(0);
			modelBuilder.Entity<SecondHand>().Property(p => p.DeletedMark).HasDefaultValue(0);

			modelBuilder.Entity<Hire>().Property(p => p.IsAccepted).HasDefaultValue(0);
			modelBuilder.Entity<Hire>().Property(p => p.IsCompleted).HasDefaultValue(0);
			modelBuilder.Entity<Hire>().Property(p => p.DeletedMark).HasDefaultValue(0);

		}
	}
}
