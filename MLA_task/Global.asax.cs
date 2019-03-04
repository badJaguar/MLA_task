using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MLA_task
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);

            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}
