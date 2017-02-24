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
    public class CategoryController : ApiController
    {
		private CategoryService categoryService = new CategoryService();
        // GET api/v1/categories
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        public IEnumerable<categories> Get()
        {
            IRepository<categories> repo = new Repository<categories>();
            return repo.GetAll<categories>(); 
        }

        // GET api/v1/categories/5
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        public categories Get(int id)
        {
            IRepository<categories> repo = new Repository<categories>();
            return repo.GetReference<categories>(id);
        }

        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [Route("api/v1/category/{id}/places")]
        public IEnumerable<place> GetCategoryPlaces(int id)
        {
			IEnumerable<place> placesToReturn = categoryService.GetCategoryPlaces(id);
            return placesToReturn.Distinct().Take<place>(99);
        }

		[CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
		[Route("api/v1/category/places")]
		public IEnumerable<place> GetCategoryPlaces(String ids)
		{
			List<place> placesToReturn = new List<place>();
			if(!String.IsNullOrEmpty(ids))
			{ 
				foreach (String id in ids.Split(','))
				{
					var intId = Convert.ToInt32(id);
					placesToReturn.AddRange(categoryService.GetCategoryPlaces(intId));
				}
			}

			return placesToReturn.Distinct().OrderBy(p => p.prime_name).Take<place>(99);
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
