using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CampusResourceSharingPlatform.Model.Business
{
	public class Hire:MissionDetail
	{
		/// <summary>
		/// 物品照片url
		/// </summary>
		[Required]
		public string GoodsPhotoUrl { get; set; }

		/// <summary>
		/// 物品名称
		/// </summary>
		[Required]
		public string GoodsName { get; set; }

		/// <summary>
		/// 物品定价 索赔用
		/// </summary>
		[Required]
		public double GoodsPrice { get; set; }

		/// <summary>
		/// 物品描述
		/// </summary>
		[Required]
		public string GoodsDescription { get; set; }

		/// <summary>
		/// 分类
		/// </summary>
		public string GoodsCategory { get; set; }
		
		/// <summary>
		/// 租金
		/// </summary>
		[Required]
		public double GoodsRent { get; set; }
		
		/// <summary>
		/// 时限
		/// </summary>
		[Required]
		public double TimeLimit { get; set; }

		/// <summary>
		/// 到期日期 接受时触发
		/// </summary>
		public DateTime? ExpiredTime { get; set; }
	}
}
