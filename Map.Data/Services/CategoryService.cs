using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map.Models;
using System.Web.Script.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using NHibernate;

namespace Map.Data.Services
{
    public class CategoryService
    {
		public IEnumerable<place> GetCategoryPlaces(int CategoryId, int CampusId = 1)
		{
			IRepository<categories> repo = new Repository<categories>();
			CampusService campusService = new CampusService();
			campus thisCampus = campusService.get(CampusId);
			var category = repo.GetReference<categories>(CategoryId);
			List<place> placesToReturn = new List<place>();
			placesToReturn.AddRange(category.Places);

			foreach (var child in category.Children)
			{
				placesToReturn.AddRange(child.Places);
			}

			placesToReturn = placesToReturn.FindAll(p => p.campus.id == thisCampus.id);
			return placesToReturn.Distinct<place>().OrderBy(p => p.prime_name);
		}
	}
}
