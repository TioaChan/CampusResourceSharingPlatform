using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Interface
{
	public interface ILicensesDateService<T> where T : class
	{
		List<T> GetAll();
		Task<List<T>> GetAllAsync();
	}
}
