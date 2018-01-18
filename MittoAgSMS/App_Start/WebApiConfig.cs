using MittoAgSMS.BusinessLogic.Abstractions;
using MittoAgSMS.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using MittoAgSMS.DataAccessLayer;
using MittoAgSMS.DataAccessLayer.Abstraction;
using Autofac;
using Autofac.Builder;
using Autofac.Features;
using Autofac.Util;
using System.Reflection;
using MittoAgSMS.Services;

namespace MittoAgSMS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            MapRoutes(config);
        }

        private static void MapRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "SMS",
            routeTemplate: "{controller}/{action}.{ext}");

            config.Routes.MapHttpRoute(
            name: "Common",
            routeTemplate: "{action}.{ext}",
            defaults: new { controller = "Common" });

            config.Formatters.XmlFormatter.AddUriPathExtensionMapping("xml", "application/xml");
            config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");
        }
    }
}
