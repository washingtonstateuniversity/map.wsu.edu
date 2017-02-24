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
    public class PlaceController : ApiController
    {
		private IPlaceService placeService;
		public PlaceController(IPlaceService _placeservice)
		{
			placeService = _placeservice;
		}
		
        // GET api/v1/place
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        public IEnumerable<place> Get()
        {
            return placeService.GetAll(); 
        }

        // GET api/v1/place/search?query=xyz
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [Route("api/v1/place/search")]
        [HttpGet]
        public IEnumerable<searchPlace> Search(String query)
        {
            return placeService.Search(query);
        }

        // GET api/v1/place/5
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        public place Get(int id)
        {
            return placeService.Get(id);
        }

		/*
        // POST api/v1/place
        [WSUBasicAuthorization]
        public void Post([FromBody]place value)
        {
        }

        // PUT api/v1/place/5
        [WSUBasicAuthorization]
        public void Put(int id, [FromBody]place value)
        {
        }

        // DELETE api/v1/place/5
        [WSUBasicAuthorization]
        public void Delete(int id)
        {

        }
		*/
    }
}
