using CampusResourceSharingPlatform.Model.Application;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CampusResourceSharingPlatform.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<MissionType> MissionTypes { get; set; }

		public DbSet<Express> MissionExpresses { get; set; }

		public DbSet<License> ThirdLicenses { get; set; }

		public DbSet<Purchase> MissionPurchase { get; set; }

		public DbSet<SecondHand> MissionFleaMarket { get; set; }

		public DbSet<Hire> MissionHire { get; set; }

		public DbSet<ExpressCompanyList> ExpressCompanyList { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			#region Identity
			modelBuilder.Entity<ApplicationUser>(b =>
			{
				// Each User can have many UserClaims
				b.HasMany(e => e.Claims)
					.WithOne()
					.HasForeignKey(uc => uc.UserId)
					.IsRequired();

				// Each User can have many UserLogins
				b.HasMany(e => e.Logins)
					.WithOne()
					.HasForeignKey(ul => ul.UserId)
					.IsRequired();

				// Each User can have many UserTokens
				b.HasMany(e => e.Tokens)
					.WithOne()
					.HasForeignKey(ut => ut.UserId)
					.IsRequired();

				// Each User can have many entries in the UserRole join table
				b.HasMany(e => e.UserRoles)
					.WithOne()
					.HasForeignKey(ur => ur.UserId)
					.IsRequired();
			});
			#endregion

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

			modelBuilder.Entity<MissionType>().Property(p => p.DeletedMark).HasDefaultValue(0);
			modelBuilder.Entity<ApplicationUser>().Property(p => p.DeletedMark).HasDefaultValue(0);

			modelBuilder.Entity<Express>(b =>
			{
				b.Property(p => p.IsAccepted).HasDefaultValue(0);
				b.Property(p => p.IsCompleted).HasDefaultValue(0);
				b.Property(p => p.DeletedMark).HasDefaultValue(0);
				b.Property(p => p.ExpressCompanyId)
					.HasDefaultValue("00000000-0000-0000-0000-000000000001");
			});

			modelBuilder.Entity<Purchase>(b =>
			{
				b.Property(p => p.IsAccepted).HasDefaultValue(0);
				b.Property(p => p.IsCompleted).HasDefaultValue(0);
				b.Property(p => p.DeletedMark).HasDefaultValue(0);
			});

			modelBuilder.Entity<SecondHand>(b =>
			{
				b.Property(p => p.IsAccepted).HasDefaultValue(0);
				b.Property(p => p.IsCompleted).HasDefaultValue(0);
				b.Property(p => p.DeletedMark).HasDefaultValue(0);
			});

			modelBuilder.Entity<Hire>(b =>
			{
				b.Property(p => p.IsAccepted).HasDefaultValue(0);
				b.Property(p => p.IsCompleted).HasDefaultValue(0);
				b.Property(p => p.DeletedMark).HasDefaultValue(0);
			});

			modelBuilder.Entity<ExpressCompanyList>(b =>
			{
				b.Property(p => p.DeletedMark).HasDefaultValue(0);
			});
		}
	}
}
