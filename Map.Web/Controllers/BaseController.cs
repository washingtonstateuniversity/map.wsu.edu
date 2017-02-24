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
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.IO;


namespace Map.Controllers
{
    
    public class BaseController : ApiController
    {
        public HttpResponseMessage JsonString(String json)
        {
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }
        public HttpResponseMessage SVGString(String json)
        {
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "image/svg+xml");
            return response;
        }
    }
}
