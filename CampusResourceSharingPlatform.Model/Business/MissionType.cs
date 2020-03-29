using System.ComponentModel.DataAnnotations;

namespace CampusResourceSharingPlatform.Model.Business
{
	public class MissionType : MissionBase
	{
		/// <summary>
		/// 任务类目名称
		/// </summary>
		[Required]
		public string TypeName { get; set; }

	}
}
