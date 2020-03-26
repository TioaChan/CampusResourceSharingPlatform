using CampusResourceSharingPlatform.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Model;
using Microsoft.EntityFrameworkCore;

namespace CampusResourceSharingPlatform.Service
{
	public class LicenseDateService:ILicensesDateService<License>
	{
		private readonly ApplicationDbContext _context;

		public LicenseDateService(ApplicationDbContext context)
		{
			_context = context;
		}

		public List<License> GetAll()
		{
			return _context.ThirdLicenses.ToList();
		}

		public async Task<List<License>> GetAllAsync()
		{
			var list =await _context.ThirdLicenses.ToListAsync();
			return list;
		}
	}
}
