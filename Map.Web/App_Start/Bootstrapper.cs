using System.Web.Http;
using System.Web.Mvc;

using Map.Data.Services;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace Map.Web
{
	public static class Bootstrapper
	{
		public static void Initialize()
		{
			var container = BuildUnityContainer();
			var resolver = new UnityDependencyResolver(container);
			DependencyResolver.SetResolver(resolver);
			GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
		}

		private static IUnityContainer BuildUnityContainer()
		{
			var container = new UnityContainer();

			// register all your components with the container here
			// e.g. container.RegisterType<itestservice, testservice="">();
			container.RegisterType<IPlaceService, PlaceService>();
			container.RegisterType<ICampusService, CampusService>();
			container.RegisterType<ISmallUrlService, SmallUrlService>();

			return container;
		}
	}
}