using System.ComponentModel.DataAnnotations;

namespace CampusResourceSharingPlatform.Web.Models
{
	public class MissionBase
	{
		/// <summary>
		/// Id
		/// </summary>
		[Required]
		public string Id { get; set; }

		/// <summary>
		/// 删除标记
		/// </summary>
		[Required]
		public bool DeletedMark { get; set; }
	}
}
