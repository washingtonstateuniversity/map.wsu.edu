using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map.Data.Services;
using Map.Models;

namespace Map.Tests.Services
{
	public class CampusTestService : ICampusService
	{
		private List<campus> campusList = new List<campus>();

		public CampusTestService()
		{
			this.campusList.Add(new campus(1, "Pullman"));
			this.campusList.Add(new campus(2, "Spokane"));
		}

		public campus get(int id)
		{
			foreach (campus oneplace in campusList)
			{
				if (oneplace.id == id)
					return oneplace;
			}

			return null;
		}

		public IEnumerable<campus> GetAll()
		{
			return campusList;
		}
	}
}
