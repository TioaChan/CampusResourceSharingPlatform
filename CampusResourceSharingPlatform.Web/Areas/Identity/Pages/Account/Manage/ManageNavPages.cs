using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CampusResourceSharingPlatform.Web.Areas.Identity.Pages.Account.Manage
{
	public static class ManageNavPages
	{
		public static string Index => "Index";

		public static string Email => "Email";

		public static string Phone => "Phone";

		public static string ChangePassword => "ChangePassword";

		public static string ExternalLogins => "ExternalLogins";

		public static string PersonalData => "PersonalData";

		public static string TwoFactorAuthentication => "TwoFactorAuthentication";

		public static string StudentIdentity => "StudentIdentity";

		public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

		public static string EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);

		public static string PhoneNavClass(ViewContext viewContext) => PageNavClass(viewContext, Phone);

		public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

		public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

		public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

		public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

		public static string StudentIdentityClass(ViewContext viewContext) => PageNavClass(viewContext, StudentIdentity);
		private static string PageNavClass(ViewContext viewContext, string page)
		{
			var activePage = viewContext.ViewData["ActivePage"] as string
				?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
			return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
		}
	}
}
