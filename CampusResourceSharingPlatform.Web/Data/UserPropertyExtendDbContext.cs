using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CampusResourceSharingPlatform.Web.Data
{
	public class UserPropertyExtendDbContext: IdentityDbContext<UserPropertyExtend>
	{
		public UserPropertyExtendDbContext(DbContextOptions<UserPropertyExtendDbContext> options):base(options)
		{
			
		}
	}
}
