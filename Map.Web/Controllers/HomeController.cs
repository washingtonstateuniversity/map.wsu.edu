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

		public ActionResult Index(String[] cat, String pid, int campus = 1, String directionsmode = "", String directionsplaceid = "")
        {
			campus thisCampus = campusService.get(campus);
			ViewBag.campusid = campus;
			ViewBag.city = thisCampus.city;
			ViewBag.latitude = thisCampus.latitude;
			ViewBag.longitude = thisCampus.longitude;
			ViewBag.directionsmode = directionsmode;
			ViewBag.directionsplaceid = directionsplaceid;

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
			String pid = "", directionsmode = "", directionsplaceid = "";
			int campusid = 1;

			if (matchingurl != null)
			{
				ViewBag.smallurl = matchingurl.or_url;
				cat = getCategoryFromURL(matchingurl.or_url);
				pid = getPlaceIDFromURL(matchingurl.or_url);
				directionsmode = getDirectionsModeFromURL(matchingurl.or_url);
				directionsplaceid = getDirectionsPlaceIDFromURL(matchingurl.or_url);
				campusid = getCampusFromURL(matchingurl.or_url);
			}

			return Index(cat, pid, campusid, directionsmode, directionsplaceid);
		}

		public ActionResult campus(int campusid)
		{
			return Index(null, null, campusid, null, null);
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

		private String getDirectionsPlaceIDFromURL(String originalurl)
		{
			originalurl = originalurl.Replace("/central?", "");
			if (!originalurl.Contains("directionsplaceid="))
				return "";
			var parsed = HttpUtility.ParseQueryString(originalurl);
			return parsed["directionsplaceid"];
		}

		private String getDirectionsModeFromURL(String originalurl)
		{
			originalurl = originalurl.Replace("/central?", "");
			if (!originalurl.Contains("directionsmode="))
				return "";
			var parsed = HttpUtility.ParseQueryString(originalurl);
			return parsed["directionsmode"];
		}

		private int getCampusFromURL(String originalurl)
		{
			originalurl = originalurl.Replace("/central?", "");
			if (!originalurl.Contains("campusid="))
				return 1;
			var parsed = HttpUtility.ParseQueryString(originalurl);
			int parsedint = 1;
			Int32.TryParse(parsed["campusid"], out parsedint);
			return parsedint;
		}
	}
}
