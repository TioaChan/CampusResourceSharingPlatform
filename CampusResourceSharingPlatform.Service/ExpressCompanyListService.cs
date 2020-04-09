using CampusResourceSharingPlatform.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Data;
using CampusResourceSharingPlatform.Interface;
using Microsoft.EntityFrameworkCore;

namespace CampusResourceSharingPlatform.Service
{
	public class ExpressCompanyListService: IExpressCompanyListService<ExpressCompanyList>
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
	}
}
