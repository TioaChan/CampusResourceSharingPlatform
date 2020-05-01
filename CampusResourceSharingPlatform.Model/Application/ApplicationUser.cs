using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampusResourceSharingPlatform.Model.Application
{
	public class ApplicationUser : IdentityUser
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
		public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
		public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
		public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
		public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
	}
}
