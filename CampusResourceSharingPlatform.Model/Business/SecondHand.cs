using System.ComponentModel.DataAnnotations;

namespace CampusResourceSharingPlatform.Model.Business
{
	public class SecondHand : MissionDetail
	{
		/// <summary>
		/// 物品照片url
		/// </summary>
		[Required]
		[MaxLength(256)]
		public string GoodsPhotoUrl { get; set; }

		/// <summary>
		/// 物品名称
		/// </summary>
		[Required]
		[MaxLength(32)]
		public string GoodsName { get; set; }

		/// <summary>
		/// 物品定价
		/// </summary>
		[Required]
		public double GoodsPrice { get; set; }

		/// <summary>
		/// 物品质量
		/// </summary>
		[Required]
		[MaxLength(8)]
		public string GoodsQuality { get; set; }

		/// <summary>
		/// 物品描述
		/// </summary>
		[Required]
		[MaxLength(256)]
		public string GoodsDescription { get; set; }

	}
}
