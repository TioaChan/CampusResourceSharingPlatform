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
	}
}
