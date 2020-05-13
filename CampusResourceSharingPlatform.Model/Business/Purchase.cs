using System.ComponentModel.DataAnnotations;

namespace CampusResourceSharingPlatform.Model.Business
{
	public class Purchase : MissionDetail
	{
		/// <summary>
		/// 购买内容
		/// </summary>
		[Required]
		[MaxLength(256)]
		public string PurchaseContent { get; set; }

		/// <summary>
		/// 购买地址
		/// </summary>
		[Required]
		[MaxLength(128)]
		public string PurchaseAddress { get; set; }

		/// <summary>
		/// 购买需求
		/// </summary>
		[Required]
		[MaxLength(128)]
		public string PurchaseRequirement { get; set; }
	}
}
