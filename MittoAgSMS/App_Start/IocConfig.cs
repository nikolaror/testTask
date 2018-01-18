using MittoAgSMS.BusinessLogic.Abstractions;
using MittoAgSMS.DataAccessLayer;
using MittoAgSMS.DataAccessLayer.Abstraction;
using MittoAgSMS.Services;
using MittoAgSMS.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Builder;
using Autofac.Features;
using Autofac.Util;
using Autofac.Integration.WebApi;
using System.Data.Entity;

namespace MittoAgSMS
{
    public class IocConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ModelEntities>().InstancePerRequest();
            builder.RegisterType<BusinessLogic.SendSmsBusinessLogic>().As<ISendSmsBusinessLogic>().InstancePerDependency();
            builder.RegisterType<BusinessLogic.CountriesBusinessLogic>().As<ICountriesBusinessLogic>().InstancePerDependency();
            builder.RegisterType<BusinessLogic.StatisticsBusinessLogic>().As<IStatisticsBusinessLogic>().InstancePerDependency();

            builder.RegisterType<SendSmsServiceMock>().As<ISendSmsService>().InstancePerRequest();
            builder.RegisterType<CountriesService>().As<ICountriesService>().InstancePerRequest();
            builder.RegisterType<StatisticsService>().As<IStatisticsService>().InstancePerRequest();
            builder.RegisterType<GetSentSmsService>().As<IGetSentSmsService>().InstancePerRequest();
            
            builder.RegisterType<SentSmsRepository>().As<ISentSmsRepository>().InstancePerRequest();
            builder.RegisterType<CountriesRepository>().As<ICountriesRepository>().InstancePerRequest();
            builder.RegisterType<StatisticsRepository>().As<IStatisticsRepository>().InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}