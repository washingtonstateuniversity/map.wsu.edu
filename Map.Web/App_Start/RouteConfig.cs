namespace Map
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;

	public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
			   name: "mapshortcodet",
			   url: "t/{key}",
			   defaults: new { controller = "Home", action = "mapshortcode", key = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "spokane",
				url: "spokane",
				defaults: new { controller = "Home", action = "campus", campusid = 4 }
			);

			routes.MapRoute(
				name: "vancouver",
				url: "vancouver",
				defaults: new { controller = "Home", action = "campus", campusid = 3 }
			);

			routes.MapRoute(
				name: "richland",
				url: "richland",
				defaults: new { controller = "Home", action = "campus", campusid = 2 }
			);

			routes.MapRoute(
				name: "pullman",
				url: "pullman",
				defaults: new { controller = "Home", action = "campus", campusid = 1 }
			);

			routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
		}
    }
}
