using System.ComponentModel.DataAnnotations;

namespace CampusResourceSharingPlatform.Model
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
