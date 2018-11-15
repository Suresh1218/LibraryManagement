using Autofac;
using Autofac.Integration.WebApi;
using LibraryServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LibraryManagement
{
    public class BootStapper
    {
        public static void Configure()
        {
            ConfigurationAutoFacContainer();
            MapRegister.Configure();
        }
        public static void ConfigurationAutoFacContainer()
        {
            var WebApiContainerBuilder = new ContainerBuilder();
            ConfigurationWebApiContainer(WebApiContainerBuilder);
        }
        public static void ConfigurationWebApiContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UserService>().As<IUserService>().AsImplementedInterfaces().InstancePerApiRequest();
            containerBuilder.RegisterType<BookService>().As<IBookService>().AsImplementedInterfaces().InstancePerApiRequest();

            containerBuilder.RegisterApiControllers(System.Reflection.Assembly.GetExecutingAssembly());
            IContainer container = containerBuilder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}