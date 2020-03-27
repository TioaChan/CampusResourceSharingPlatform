using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CampusResourceSharingPlatform.Model.Application
{
	public class ApplicationUser:IdentityUser
	{
		[MaxLength(18)]
		public string IdCardNo { get; set; }
		[MaxLength(12)]
		public string SchoolCardNo { get; set; }
		public bool StudentIdentityConfirmed { get; set; }
		public string ProfilePhotoUrl { get; set; }
		public string NickName { get; set; }
		public string RealName { get; set; }
		public bool DeletedMark { get; set; }

	}
}
