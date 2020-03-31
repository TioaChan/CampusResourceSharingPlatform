using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Interface
{
	public interface ITakeExpressService<T> where T : class
	{
		int Post(T newPost);

		Task<int> PostAsync(T newPost);


		/// <summary>
		/// 异步 返回最后一次提交的表单信息
		/// </summary>
		/// <param name="userId">用户id</param>
		/// <returns>T</returns>
		Task<T> GetLastMissionInfoAsync(string userId);

		/// <summary>
		/// 异步 返回所有有效任务列表（有效：当前UTC时间未超出失效时间）
		/// </summary>
		/// <returns>List</returns>
		Task<List<T>> GetAllActiveMissionAsync();

		/// <summary>
		/// 异步 按时间倒序返回前10个有效任务列表（有效：当前UTC时间未超出失效时间）
		/// </summary>
		/// <returns>List</returns>
		Task<List<T>> GetTop10ActiveMissionAsync();
	}
}
