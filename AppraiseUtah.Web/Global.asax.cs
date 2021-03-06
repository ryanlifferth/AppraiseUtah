﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AppraiseUtah.Web.Common;
using System.Web.Optimization;
using System.Data.Entity;
using AppraiseUtah.Client.Models;

namespace AppraiseUtah.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEngines.Engines.Add(new MyRazorViewEngine());

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Allows changes to the DB model......need more info on this
            Database.SetInitializer<AppraisalContext>(null);
        }
    }
}