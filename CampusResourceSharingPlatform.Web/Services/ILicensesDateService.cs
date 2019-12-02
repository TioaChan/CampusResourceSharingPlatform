using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Web.Models;

namespace CampusResourceSharingPlatform.Web.Services
{
	public interface ILicensesDateService<T> where T :class
	{
		IEnumerable<T> GetAll();
	}
}
