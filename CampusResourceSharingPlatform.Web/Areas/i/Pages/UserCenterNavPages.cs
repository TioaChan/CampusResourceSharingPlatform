using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CampusResourceSharingPlatform.Web.Areas.i.Pages
{
	public class UserCenterNavPages
	{
		public static string Index => "Index";
		public static string TakeExpress => "TakeExpress";
		public static string FleaMarket => "FleaMarket";
		public static string Purchase => "Purchase";
		public static string Hire => "Hire";
		public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);
		public static string TakeExpressNavClass(ViewContext viewContext) => PageNavClass(viewContext, TakeExpress);
		public static string FleaMarketNavClass(ViewContext viewContext) => PageNavClass(viewContext, FleaMarket);
		public static string PurchaseNavClass(ViewContext viewContext) => PageNavClass(viewContext, Purchase);
		public static string HireNavClass(ViewContext viewContext) => PageNavClass(viewContext, Hire);
		private static string PageNavClass(ViewContext viewContext, string page)
		{
			var activePage = viewContext.ViewData["ActivePageOfPost"] as string
							 ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
			return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
		}
	}
}
