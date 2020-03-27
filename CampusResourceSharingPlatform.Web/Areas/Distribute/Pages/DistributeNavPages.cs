using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CampusResourceSharingPlatform.Web.Areas.Distribute.Pages
{
	public class DistributeNavPages
	{
		public static string Take => "Take";

		public static string TakeOrder => "TakeOrder";

		public static string Buy => "Buy";

		public static string TakeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Take);
		public static string TakeOrderNavClass(ViewContext viewContext) => PageNavClass(viewContext, TakeOrder);

		public static string BuyNavClass(ViewContext viewContext) => PageNavClass(viewContext, Buy);

		private static string PageNavClass(ViewContext viewContext, string page)
		{
			var activePage = viewContext.ViewData["ActivePage"] as string
							 ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
			return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
		}
	}
}
