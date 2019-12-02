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
		public int SchoolCardNo { get; set; }
	}
}
