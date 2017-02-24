using System.Collections.Generic;
using Map.Models;

namespace Map.Data.Services
{
	public interface IPlaceService
	{
		place Get(int id);
		IEnumerable<place> GetAll();
		IEnumerable<searchPlace> Search(string query);
	}
}