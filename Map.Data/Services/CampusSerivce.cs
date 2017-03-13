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
    public class CampusService : ICampusService
	{
		public campus get(int id)
		{
			IRepository<campus> campusRepo = new Repository<campus>();
			return campusRepo.GetReference<campus>(id);
		}
	}
}
