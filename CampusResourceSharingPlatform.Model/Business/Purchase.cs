using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CampusResourceSharingPlatform.Model.Business
{
	public class Purchase:MissionDetail
	{
		/// <summary>
		/// 购买内容
		/// </summary>
		[Required]
		public string PurchaseContent { get; set; }

		/// <summary>
		/// 购买地址
		/// </summary>
		[Required]
		public string PurchaseAddress { get; set; }

		/// <summary>
		/// 购买需求
		/// </summary>
		[Required]
		public string PurchaseRequirement { get; set; }
	}
}
