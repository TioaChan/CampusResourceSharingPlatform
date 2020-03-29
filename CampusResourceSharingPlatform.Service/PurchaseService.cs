using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CampusResourceSharingPlatform.Interface;
using CampusResourceSharingPlatform.Model.Business;

namespace CampusResourceSharingPlatform.Service
{
	public class PurchaseService:IPurchaseService<Purchase>
	{
		public int Post(Purchase newPost)
		{
			throw new NotImplementedException();
		}

		public Task<int> PostAsync(Purchase newPost)
		{
			throw new NotImplementedException();
		}
	}
}
