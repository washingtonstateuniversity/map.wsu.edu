using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map.Data.Services;
using Map.Models;

namespace Map.Tests.Services
{
	public class PlaceTestService : IPlaceService
	{
		private List<place> placeList = new List<place>();
		public PlaceTestService()
		{
			this.placeList.Add(new place(1, "Steam plant"));
			this.placeList.Add(new place(2, "Martin Stadium"));
			this.placeList.Add(new place(3, "CUB"));
			this.placeList.Add(new place(4, "REC Center"));
		}

		public place Get(int id)
		{
			foreach (place oneplace in placeList)
			{
				if (oneplace.id == id)
					return oneplace;
			}

			return null;
		}

		public IEnumerable<place> GetAll()
		{
			return placeList;
		}

		public IEnumerable<searchPlace> Search(string query)
		{
			List<searchPlace> returnlist = new List<searchPlace>();
			foreach (place oneplace in placeList)
			{
				if (!String.IsNullOrEmpty(oneplace.prime_name) && oneplace.prime_name.Contains(query))
				{
					returnlist.Add(new searchPlace(oneplace.prime_name, oneplace.prime_name, oneplace.id));
				}
			}

			return returnlist;
		}
	}
}
