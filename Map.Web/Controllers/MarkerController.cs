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
using System.Text;

namespace Map.Controllers
{
    public class MarkerController : BaseController
    {
        // GET api/v1/marker/id
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [HttpGet]
        [Route("api/v1/marker/{id}")]
        public HttpResponseMessage Get(int id)
        {
            String svg = "<?xml version='1.0' encoding='utf-8'?><!-- Generator: Adobe Illustrator 18.1.1, SVG Export Plug-In . SVG Version: 6.00 Build 0)  --><svg version='1.1' id='Layer_2' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' x='0px' y='0px' viewBox='104 23 429.5 739' enable-background='new 104 23 429.5 739' xml:space='preserve'><path fill='#9E1D32' stroke='#FFFFFF' stroke-width='6' stroke-miterlimit='10' d='M529.7,237.8c0,116.1-196.6,521.1-210.4,521.1 c-16.9,0-210.4-404.8-210.4-521.1S203,27.4,319.3,27.4S529.7,121.5,529.7,237.8z'/><text class='marker-number' transform='matrix(1 0 0 1 133.6372 294.8559)' x='180' y='0' text-anchor='middle'  fill='#F1F1F1' font-size='225' >" + id + "</text></svg>";
            return SVGString(svg);
        }
    }
}
