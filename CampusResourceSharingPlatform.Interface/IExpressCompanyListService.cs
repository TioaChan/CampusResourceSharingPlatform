using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Interface
{
	public interface IExpressCompanyListService<T> where T : class
	{
		Task<List<T>> GetAllAsync();
	}
}
