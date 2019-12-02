using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Web.Models
{
	public class License
	{
		public int Id { get; set; }

		[Display(Name = "开源项目名称")]
		public string LicenseName { get; set; }

		[Display(Name = "项目许可类型")]
		public string LicenseType { get; set; }

		[Display(Name = "项目地址")]
		public string RepoUrl { get; set; }
	}
}
