using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Interface
{
	public interface IHireService<T> where T : class
	{
		int Post(T newPost);

		Task<int> PostAsync(T newPost);
	}
}
