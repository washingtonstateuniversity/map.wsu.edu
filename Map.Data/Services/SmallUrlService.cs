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
    public class SmallUrlService : ISmallUrlService
	{
		public small_url getByKey(String key)
		{
			IRepository<small_url> small_urlRepo = new Repository<small_url>();
			return small_urlRepo.GetFirstByProperty<small_url>("sm_url", key);
		}
	}
}
