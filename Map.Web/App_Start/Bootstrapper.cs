using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Map.Data.Services;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Web.Mvc;
using System.Web.Http;


namespace Map.Web
{
	public static class Bootstrapper
	{
		public static void Initialize()
		{
			var container = BuildUnityContainer();
			var resolver = new UnityDependencyResolver(container);
			DependencyResolver.SetResolver( resolver );
			GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
		}

		private static IUnityContainer BuildUnityContainer()
		{
			var container = new UnityContainer();

			// register all your components with the container here
			// e.g. container.RegisterType<itestservice, testservice="">();            
			container.RegisterType<IPlaceService, PlaceService>();

			return container;
		}
	}

}