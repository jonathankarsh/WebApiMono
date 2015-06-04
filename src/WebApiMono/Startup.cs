using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Owin;
using WebApiMono.Controllers;

namespace WebApiMono {
    public class Startup {
        public void Configuration(IAppBuilder appBuilder) {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.SuppressDefaultHostAuthentication();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IHttpControllerTypeResolver), new HttpControllerTypeResolver());
            appBuilder.UseWebApi(config);
        }

        private class HttpControllerTypeResolver : IHttpControllerTypeResolver {
            public ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver) {
                var httpControllerType = typeof (IHttpController);
                var controllerTypes = typeof(ValuesController)
                    .Assembly
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && httpControllerType.IsAssignableFrom(t))
                    .ToList();
                return controllerTypes;
            }
        }
    }
}