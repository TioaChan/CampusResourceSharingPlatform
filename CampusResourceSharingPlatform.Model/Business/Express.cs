using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusResourceSharingPlatform.Model.Business
{
	public class Express : MissionDetail
	{
		/// <summary>
		/// 快递公司
		/// </summary>
		[Required]
		public string ExpressCompany { get; set; }

		/// <summary>
		/// 快递单号
		/// </summary>
		[Required]
		public string TrackingCode { get; set; }

		/// <summary>
		/// 快递收件人姓名
		/// </summary>
		[Required]
		public string Consignee { get; set; }

		/// <summary>
		/// 快递收件人手机号【后四位？】
		/// </summary>
		[Required]
		public string ConsigneePhone { get; set; }


		/// <summary>
		/// 菜鸟驿站取货码
		/// </summary>
		[Required]
		public string PickCode { get; set; }

		/// <summary>
		/// 菜鸟驿站地址
		/// </summary>
		[Required]
		public string YiZhanName { get; set; }

		/// <summary>
		/// 包裹重量 单位:千克
		/// </summary>
		[Required]
		public double Weight { get; set; }

		/// <summary>
		/// 快递公司
		/// </summary>
		[Required]
		public string ExpressCompanyId { get; set; }
		
		[ForeignKey("ExpressCompanyId")]
		public virtual ExpressCompanyList ExpressCompany2 { get; set; }
	}
}
