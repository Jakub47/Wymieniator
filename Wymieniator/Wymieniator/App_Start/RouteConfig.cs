﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Wymieniator.DAL;

namespace Wymieniator
{
    public class RouteConfig
    {
        private static  WymieniatorContext db = new WymieniatorContext();
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "BooksList",
                url: "Kategoria/{nameOfCategory}",
                defaults: new { controller = "Book", action = "ListOfBooksInCategory" }
            );

            routes.MapRoute(
                name: "BookDetail",
                url: "SzczegolyKsiazki-{id}",
                defaults: new { controller = "Book", action = "Detail" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
