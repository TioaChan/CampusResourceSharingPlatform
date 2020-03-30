using System;
using System.Collections.Generic;
using System.Text;

namespace CampusResourceSharingPlatform.Model.Business
{
	public class SecondHand:MissionDetail
	{
		public string GoodsPhotoUrl { get; set; }

		public string GoodsName { get; set; }

		public double GoodsPrice { get; set; }

		public string GoodsQuality { get; set; }

		public string GoodsDescription { get; set; }

	}
}
