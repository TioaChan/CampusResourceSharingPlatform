using System.ComponentModel.DataAnnotations;

namespace CampusResourceSharingPlatform.Model.Application
{
	public class License
	{
		[MaxLength(64)]
		public string Id { get; set; }

		[MaxLength(64)]
		[Display(Name = "开源项目名称")]
		public string LicenseName { get; set; }

		[MaxLength(128)]
		[Display(Name = "项目许可类型")]
		public string LicenseType { get; set; }

		[MaxLength(256)]
		[Display(Name = "项目地址")]
		public string RepoUrl { get; set; }
	}
}
