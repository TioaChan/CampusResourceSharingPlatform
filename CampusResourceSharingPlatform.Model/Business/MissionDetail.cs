using CampusResourceSharingPlatform.Model.Application;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusResourceSharingPlatform.Model.Business
{
	public class MissionDetail : MissionBase
	{
		#region properties
		/// <summary>
		/// 所属类目编号
		/// </summary>
		[Required]
		[MaxLength(64)]
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
		/// 备注信息
		/// </summary>
		[MaxLength(256)]
		public string MissionNotes { get; set; }

		/// <summary>
		/// 任务奖励
		/// </summary>
		[MaxLength(32)]
		public string MissionReward { get; set; }

		/// <summary>
		/// 是否被接受
		/// </summary>
		[Required]
		public bool IsAccepted { get; set; }

		/// <summary>
		/// 接受人Id
		/// </summary>
		public string AcceptUserId { get; set; }

		/// <summary>
		/// 接受时间
		/// </summary>
		public DateTime? AcceptTime { get; set; }

		/// <summary>
		/// 任务是否完成
		/// </summary>
		[Required]
		public bool IsCompleted { get; set; }


		/// <summary>
		/// 地址1
		/// </summary>
		[Required]
		[MaxLength(32)]
		public string PosterAddress1 { get; set; }

		/// <summary>
		/// 地址2
		/// </summary>
		[Required]
		[MaxLength(32)]
		public string PosterAddress2 { get; set; }

		/// <summary>
		/// 联系方式
		/// </summary>
		[Required]
		[MaxLength(13)]
		public string PosterPhoneNumber { get; set; }

		#endregion

		#region ForeignKey

		[ForeignKey("TypeId")]
		public virtual MissionType MissionType { get; set; }

		[ForeignKey("AcceptUserId")]
		public virtual ApplicationUser AcceptUser { get; set; }

		[ForeignKey("PostUserId")]
		public virtual ApplicationUser PostUser { get; set; }

		#endregion

	}
}
