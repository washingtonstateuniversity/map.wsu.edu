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
		public IEnumerable<place> GetCategoryPlaces(int CategoryId)
		{
			IRepository<categories> repo = new Repository<categories>();
			var category = repo.GetReference<categories>(CategoryId);
			List<place> placesToReturn = new List<place>();
			placesToReturn.AddRange(category.Places);
			/*var parent = category.Parent;
			while (parent != null)
			{
				placesToReturn.AddRange(parent.Places);
				parent = parent.Parent;
			}*/

			return placesToReturn.Distinct<place>().OrderBy(p => p.prime_name);
		}
	}
}
