using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampusResourceSharingPlatform.Web.Models
{
	public class MissionType:MissionBase
	{
		/// <summary>
		/// 任务类目名称
		/// </summary>
		[Required]
		public string TypeName { get; set; }


		public List<MissionDetail> MissionDetails { get; set; }

	}
}
