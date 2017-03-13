using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Map.Data;
using Map.Models;
using Map.Data.Services;

namespace Map.Controllers
{
    public class HomeController : Controller
    {
		private ISmallUrlService smallUrlService;
		private ICampusService campusService;

		public HomeController(ISmallUrlService _smallUrlService, ICampusService _campusService)
		{
			this.smallUrlService = _smallUrlService;
			this.campusService = _campusService;
		}

		public ActionResult Index(String[] cat, String pid)
        {
			campus thisCampus = campusService.get(1);
			ViewBag.city = thisCampus.city;
			ViewBag.latitude = thisCampus.latitude;
			ViewBag.longitude = thisCampus.longitude;
			if (Request != null)
				ViewBag.siteroot = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
			// Set selected categories
			if (cat != null)
				ViewBag.categories = String.Join(",", cat);
			// Set selected place
			ViewBag.placeid = pid;

			return View("Index");
        }

		public ActionResult mapshortcode(String key)
		{
			ViewBag.key = key;
			small_url matchingurl = smallUrlService.getByKey(key);
			String[] cat = new String[] { };
			String pid = "";

			if (matchingurl != null)
			{
				ViewBag.smallurl = matchingurl.or_url;
				cat = getCategoryFromURL(matchingurl.or_url);
				pid = getPlaceIDFromURL(matchingurl.or_url);
			}

			return Index(cat, pid);
		}

		private String[] getCategoryFromURL(String originalurl)
		{
			originalurl = originalurl.Replace("/central?", "");
			if (!originalurl.Contains("cat[]="))
				return new String[] { };
			var parsed = HttpUtility.ParseQueryString(originalurl);
			if (parsed["cat[]"] != null)
				return parsed["cat[]"].Split(',');
			return new String[] { };
		}

		private String getPlaceIDFromURL(String originalurl)
		{
			originalurl = originalurl.Replace("/central?", "");
			if (!originalurl.Contains("pid="))
				return "";
			var parsed = HttpUtility.ParseQueryString(originalurl);
			return parsed["pid"];
		}
	}
}
