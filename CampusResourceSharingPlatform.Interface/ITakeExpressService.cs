using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Interface
{
	public interface ITakeExpressService<T> where T:class
	{
		int Post(T newPost);

		Task<int> PostAsync(T newPost);
	}
}
