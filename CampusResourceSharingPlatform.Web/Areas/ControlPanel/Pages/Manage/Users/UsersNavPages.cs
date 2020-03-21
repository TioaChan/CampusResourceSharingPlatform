using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CampusResourceSharingPlatform.Web.Areas.ControlPanel.Pages.Manage.Users
{
    public static class UsersNavPages
    {
	    public static string Index => "Index";

	    public static string RoleIndex => "RoleIndex";

	    public static string UsersIndex => "UsersIndex";

		public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);
	    public static string RoleIndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, RoleIndex);

	    public static string UsersIndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, UsersIndex);

		private static string PageNavClass(ViewContext viewContext, string page)
	    {
		    var activePage = viewContext.ViewData["ActivePage"] as string
		                     ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
		    return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
	    }
    }
}
