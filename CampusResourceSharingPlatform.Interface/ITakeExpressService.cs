using CampusResourceSharingPlatform.Model.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Interface
{
	public interface ITakeExpressService<T> where T : class
	{
		int Post(T newPost);

		Task<int> PostAsync(T newPost);

		/// <summary>
		/// 异步 更新
		/// </summary>
		/// <param name="newPost"></param>
		/// <returns></returns>
		int Update(T newPost);

		/// <summary>
		/// 异步 返回最后一次提交的表单信息
		/// </summary>
		/// <param name="userId">用户id</param>
		/// <returns>T</returns>
		Task<T> GetLastMissionInfoAsync(string userId);

		/// <summary>
		/// 异步 返回所有有效任务（未标记删除且当前UTC时间未超出失效时间）
		/// </summary>
		/// <returns>List</returns>
		Task<List<T>> GetAllActiveMissionAsync();

		/// <summary>
		/// 异步 返回user所有有效任务（未标记删除且当前UTC时间未超出失效时间）
		/// </summary>
		/// <param name="user">Application User</param>
		/// <returns>List</returns>
		Task<List<T>> GetAllActiveMissionByPostUserAsync(ApplicationUser postUser);

		/// <summary>
		/// 异步 返回所有过期任务（未标记删除且当前UTC时间超出失效时间）
		/// </summary>
		/// <returns>List</returns>
		Task<List<T>> GetAllInvalidMissionAsync();

		/// <summary>
		/// 异步 返回user所有过期任务（未标记删除且当前UTC时间超出失效时间）
		/// </summary>
		/// <param name="user">Application User</param>
		/// <returns>List</returns>
		Task<List<T>> GetAllInvalidMissionByPostUserAsync(ApplicationUser postUser);

		/// <summary>
		/// 异步 返回所有标记删除的任务
		/// </summary>
		/// <returns>List</returns>
		Task<List<T>> GetAllDeletedMissionAsync();

		/// <summary>
		/// 异步 返回user所有标记删除的任务
		/// </summary>
		/// <param name="user">Application User</param>
		/// <returns>List</returns>
		Task<List<T>> GetAllDeletedMissionByPostUserAsync(ApplicationUser postUser);

		/// <summary>
		/// 异步 按时间倒序返回前10个有效任务列表（有效：当前UTC时间未超出失效时间）
		/// </summary>
		/// <returns>List</returns>
		Task<List<T>> GetTop10ActiveMissionAsync();

		/// <summary>
		/// 异步 按Id返回指定任务(含已删除任务)
		/// </summary>
		/// <param name="postId">MissionId</param>
		/// <returns>T</returns>
		Task<T> GetMissionById(string postId);

		/// <summary>
		///  异步 按Id返回未删除的任务
		/// </summary>
		/// <param name="postId"></param>
		/// <returns></returns>
		Task<T> GetActiveMissionById(string postId);
	}
}
