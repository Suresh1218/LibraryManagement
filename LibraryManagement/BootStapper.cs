using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DataModel.Infrastructure;
using DataModel.Repository;
using LibraryManagement.Controllers;
using LibraryServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

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

            var container = new ContainerBuilder();
            ConfigurationContainer(container);
        }
        public static void ConfigurationWebApiContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UserService>().As<IUserService>().AsImplementedInterfaces().InstancePerApiRequest();
            containerBuilder.RegisterType<BookService>().As<IBookService>().AsImplementedInterfaces().InstancePerApiRequest();
            
            containerBuilder.RegisterApiControllers(System.Reflection.Assembly.GetExecutingAssembly());
            IContainer container = containerBuilder.Build();
            
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        public static void ConfigurationContainer(ContainerBuilder container)
        {
            container.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register dependencies in filter attributes
            container.RegisterFilterProvider();

            // Register dependencies in custom views
            container.RegisterSource(new ViewRegistrationSource());

            container.RegisterType<DataBaseFactory>().As<IDataBaseFactory>().InstancePerRequest();
            container.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            container.RegisterType<BookService>().As<IBookService>().InstancePerRequest();
            container.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            container.RegisterType<BookRepository>().As<IBookRepository>().InstancePerRequest();
            container.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            

            //container.RegisterType<HomeController>();
            // Set the dependency resolver to be Autofac.
            var builder = container.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder));
        }
    }
}

