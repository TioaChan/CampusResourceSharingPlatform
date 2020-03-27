using System.ComponentModel.DataAnnotations;

namespace CampusResourceSharingPlatform.Model.Business
{
	public class Express:MissionDetail
	{
		[Required]
		public string ExpressCompany { get; set; }

		[Required]
		public string TrackingCode { get; set; }

		[Required]
		public string Consignee { get; set; }

		[Required]
		public int ConsigneePhone { get; set; }

		[Required]
		public string PickCode { get; set; }

		[Required]
		public string YiZhanName { get; set; }

		[Required]
		public double Weight { get; set; }
	}
}
