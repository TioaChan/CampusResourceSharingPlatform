using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusResourceSharingPlatform.Model.Business
{
	public class ExpressCompanyList : MissionBase
	{
		[MaxLength(20)]
		[Required]
		public string CompanyName { get; set; }

	}
}
