using CampusResourceSharingPlatform.Interface;
using System.Collections.Generic;
using System.Linq;
using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Model;

namespace CampusResourceSharingPlatform.Service
{
	public class LicenseDateService:ILicensesDateService<License>
	{
		private readonly ApplicationDbContext _context;

		public LicenseDateService(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<License> GetAll()
		{
			return _context.ThirdLicenses.ToList();
		}

	}
}
