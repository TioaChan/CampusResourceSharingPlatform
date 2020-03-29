using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CampusResourceSharingPlatform.Web.Areas.Distribute.Pages
{
	public static class DistributeNavPages
	{
		public static string TakeExpress => "TakeExpress";

		public static string TakeOrder => "TakeOrder";

		public static string Purchase => "Purchase";

		public static string TakeExpressNavClass(ViewContext viewContext) => PageNavClass(viewContext, TakeExpress);
		public static string TakeOrderNavClass(ViewContext viewContext) => PageNavClass(viewContext, TakeOrder);

		public static string PurchaseNavClass(ViewContext viewContext) => PageNavClass(viewContext, Purchase);

		private static string PageNavClass(ViewContext viewContext, string page)
		{
			var activePage = viewContext.ViewData["ActivePage"] as string
							 ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
			return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
		}
	}
}
