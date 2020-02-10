using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampusResourceSharingPlatform.Web.Models
{
	public class MissionDetail:MissionBase
	{
		/// <summary>
		/// 任务名称
		/// </summary>
		[Required]
		public string MissionName { get; set; }

		/// <summary>
		/// 所属类目编号
		/// </summary>
		[Required]
		public string TypeId { get; set; }

		/// <summary>
		/// 发布人Id
		/// </summary>
		[Required]
		public string PostUserId { get; set; }

		/// <summary>
		/// 任务发布时间
		/// </summary>
		[Required]
		public DateTime PostTime { get; set; }

		/// <summary>
		/// 任务设定的失效时间
		/// </summary>
		[Required]
		public DateTime InvalidTime { get; set; }

		/// <summary>
		/// 任务描述
		/// </summary>
		[Required]
		public string MissionDetails { get; set; }

		/// <summary>
		/// 任务奖励
		/// </summary>
		[Required]
		public string MissionReward { get; set; }

		/// <summary>
		/// 是否被接受
		/// </summary>
		[Required]
		public bool IsAccepted { get; set; }

		/// <summary>
		/// 接受人Id
		/// </summary>
		[Required]
		public string AcceptUserId { get; set; }

		/// <summary>
		/// 接受时间
		/// </summary>
		[Required]
		public DateTime AcceptTime { get; set; }

		public MissionType MissionType { get; set; }
	}
}
