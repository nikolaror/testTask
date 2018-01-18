using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Routing;

namespace MittoAgSMS
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IocConfig.Configure();
            AutoMapperConfig.Initialize();

            GlobalConfiguration.Configure(c=> WebApiConfig.Register(c));
        }
    }
}
