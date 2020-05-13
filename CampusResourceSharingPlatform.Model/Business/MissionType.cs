using System.ComponentModel.DataAnnotations;

namespace CampusResourceSharingPlatform.Model.Business
{
	public class MissionType : MissionBase
	{
		/// <summary>
		/// 任务类目名称
		/// </summary>
		[Required]
		[MaxLength(16)]
		public string TypeName { get; set; }

		[MaxLength(32)]
		public string TypeDescription { get; set; }

	}
}
