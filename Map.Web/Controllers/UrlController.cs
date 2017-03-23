using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Map.Models;
using Map.Data;
using Map.Web.Filters;
using WebApi.OutputCache.V2;
using Map.Data.Services;

namespace Map.Controllers
{
    public class UrlController : ApiController
    {
		// GET api/v1/url/5
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
		[Route("api/v1/url/")]
		public small_url Get(String url)
        {
            IRepository<small_url> repo = new Repository<small_url>();
			small_url foundsmallurl = repo.GetFirstByProperty<small_url>("or_url", url);
			if (foundsmallurl != null)
				return foundsmallurl;
			else {
				small_url newurl = new small_url();
				newurl.or_url = url;
				newurl.sm_url = String.Format("{0:X}", url.GetHashCode());
				repo.Save(newurl);
				repo.Commit();
				return newurl;
			}
		}

		// POST api/v1/categories
		[WSUBasicAuthorization]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/v1/categories/5
        [WSUBasicAuthorization]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/v1/categories/5
        [WSUBasicAuthorization]
        public void Delete(int id)
        {
        }
    }
}
