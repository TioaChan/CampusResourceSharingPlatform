using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Service
{
	public class ExpressCompanyListService : IExpressCompanyListService<ExpressCompanyList>
	{
		private readonly ApplicationDbContext _context;

		public ExpressCompanyListService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<List<ExpressCompanyList>> GetAllAsync()
		{
			var expressCompanyLists = await _context.ExpressCompanyList.ToListAsync();
			return expressCompanyLists;
		}

		public async Task<ExpressCompanyList> GetCompanyAsync(string companyId)
		{
			var result = await _context.ExpressCompanyList.Where(p => p.Id == companyId).FirstOrDefaultAsync();
			return result;
		}
	}
}
