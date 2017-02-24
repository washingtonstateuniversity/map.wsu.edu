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
    
    public class BusController : BaseController
    {
        [HttpGet]
        [CacheOutput(ClientTimeSpan = 150, ServerTimeSpan = 300)]
        [Route("api/v1/bus/route")]
        public HttpResponseMessage routes()
        {
            return JsonString( new WebClient().DownloadString("https://pullman.mytransitride.com/api/Route") );
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 7, ServerTimeSpan = 15)]
        [Route("api/v1/bus/location")]
        public HttpResponseMessage location(String patternIds)
        {
            String[] patternIdsArray = patternIds.Split(',');
            String url = "https://pullman.mytransitride.com/api/VehicleStatuses?patternIds%5B%5D=";
            if (patternIdsArray.Length > 0)
                url += patternIdsArray[0];
            foreach (String patternId in patternIdsArray)
            {
                if (patternId != patternIdsArray[0])
                    url += "&patternIds%5B%5D=" + patternId;
            }

            return JsonString( new WebClient().DownloadString(url) );
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 150, ServerTimeSpan = 300)]
        [Route("api/v1/bus/route/details")]
        public HttpResponseMessage getBusRouteDetails(int patternId)
        {
            String url = "https://pullman.mytransitride.com/api/PatternBuilder?patternId=" + patternId;
            return JsonString( new WebClient().DownloadString(url) );
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 150, ServerTimeSpan = 300)]
        [Route("api/v1/bus/stop/{stopId}/patterns")]
        public HttpResponseMessage getPatternsForBusStop(int stopId)
        {
            String url = "https://pullman.mytransitride.com/api/Route?StopId=" + stopId;
            return JsonString( new WebClient().DownloadString(url) );
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 150, ServerTimeSpan = 300)]
        [Route("api/v1/bus/stop/{stopId}/predictions")]
        public HttpResponseMessage getPredictionDataForBusStop(int stopId)
        {
            String url = "https://pullman.mytransitride.com/api/PredictionData?stopid=" + stopId;
            return JsonString( new WebClient().DownloadString(url) );
        }
    }
}
