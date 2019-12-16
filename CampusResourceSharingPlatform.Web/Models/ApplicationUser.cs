using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CampusResourceSharingPlatform.Web.Models
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

	}
}
