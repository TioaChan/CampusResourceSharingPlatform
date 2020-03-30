using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Interface
{
	public interface ITakeExpressService<T> where T : class
	{
		int Post(T newPost);

		Task<int> PostAsync(T newPost);

		Task<T> GetLastMissionInfoAsync(string userId);

		Task<List<T>> GetAllActiveMissionAsync();
	}
}
