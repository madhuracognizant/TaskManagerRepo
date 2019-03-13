using System;
using System.Collections.Generic;
using TaskManagerAPI.Resolvers;
using TaskManagerDAL;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using System.Data.Entity;

namespace TaskManagerAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new System.Web.Http.Cors.EnableCorsAttribute("http://localhost:4200", headers: "*", methods: "*"));

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            var container = new UnityContainer();

            container.RegisterType<IRepository<Task>, ITasksRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITasksRepository, TasksRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<DbContext, TasksContext>();

            config.DependencyResolver = new UnityResolver(container);

        }
    }
}
