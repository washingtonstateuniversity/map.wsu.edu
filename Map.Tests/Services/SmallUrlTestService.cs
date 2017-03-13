using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map.Data.Services;
using Map.Models;

namespace Map.Tests.Services
{
	public class SmallUrlTestService : ISmallUrlService
	{
		private List<small_url> smallUrlList = new List<small_url>();

		public SmallUrlTestService()
		{
			this.smallUrlList.Add(new small_url(1, "Steam plant"));
			this.smallUrlList.Add(new small_url(2, "Martin Stadium"));
			this.smallUrlList.Add(new small_url(3, "CUB"));
			this.smallUrlList.Add(new small_url(4, "REC Center"));
		}

		public small_url getByKey(String key)
		{
			foreach (small_url oneurl in smallUrlList)
			{
				if (oneurl.sm_url == key)
					return oneurl;
			}

			return null;
		}

		public IEnumerable<small_url> GetAll()
		{
			return smallUrlList;
		}
	}
}
