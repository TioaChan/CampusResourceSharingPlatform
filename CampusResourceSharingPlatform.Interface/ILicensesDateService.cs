using System.Collections.Generic;

namespace CampusResourceSharingPlatform.Interface
{
	public interface ILicensesDateService<T> where T :class
	{
		IEnumerable<T> GetAll();
	}
}
