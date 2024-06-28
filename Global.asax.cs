using StudentRegistarationPortal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentRegistarationPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            AreaRegistration.RegisterAllAreas();
            AutoMapperWebProfile.Run();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            /////temp
            ///

            ViewEngines.Engines.Clear(); // Clear the default view engines
            ViewEngines.Engines.Add(new RazorViewEngine()); // Add Razor view engine

        }
    }
}
