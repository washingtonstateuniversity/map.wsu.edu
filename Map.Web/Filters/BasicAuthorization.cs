using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using MonoRailHelper;
using Map.Data;
using Map.Models;

namespace Map.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class WSUBasicAuthorization : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            // Only filter the request if it isn't local
            if (!filterContext.ControllerContext.Request.IsLocal())
            {
                try
                {
                    IRepository<users> userRepository = new Repository<users>();
                    // If not authenticated, redirect to login
                    String username = Authentication.authenticate();
                    users authuser = userRepository.GetFirstByProperty<users>("nid", username);

                    if (authuser == null)
                    {
                        filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    }
                    else
                    {
                        filterContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                        return;
                    }
                }
                catch (Exception)
                {
                    filterContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    return;
                }
            }

            base.OnAuthorization(filterContext);
        }
    }
}