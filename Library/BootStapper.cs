using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DataModel.Infrastructure;
using DataModel.Repository;
using LibraryServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Library
{
    public class BootStapper
    {
        public static void Configuration()
        {
            var WebApiContainerBuilder = new ContainerBuilder();
            ConfigurationWebApiContainer(WebApiContainerBuilder);

            var container = new ContainerBuilder();
            ConfigurationContainer(container);
        }
        public static void ConfigurationWebApiContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DataBaseFactory>().As<IDataBaseFactory>().AsImplementedInterfaces().InstancePerApiRequest();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().AsImplementedInterfaces().InstancePerApiRequest();

            containerBuilder.RegisterType<BookRepository>().As<IBookRepository>().AsImplementedInterfaces().InstancePerApiRequest();
            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().AsImplementedInterfaces().InstancePerApiRequest();
            
            containerBuilder.RegisterType<UserService>().As<IUserService>().AsImplementedInterfaces().InstancePerApiRequest();
            containerBuilder.RegisterType<BookService>().As<IBookService>().AsImplementedInterfaces().InstancePerApiRequest();

            containerBuilder.RegisterType<UserCartRepository>().As<IUserCartRepository>().AsImplementedInterfaces().InstancePerApiRequest();
            containerBuilder.RegisterType<CartService>().As<ICartService>().AsImplementedInterfaces().InstancePerApiRequest();

            containerBuilder.RegisterType<OrderService>().As<IOrderService>().AsImplementedInterfaces().InstancePerApiRequest();
            containerBuilder.RegisterType<UserLogRepository>().As<IUserLogRepository>().AsImplementedInterfaces().InstancePerApiRequest();

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

            container.RegisterType<UserCartRepository>().As<IUserCartRepository>().InstancePerRequest();
            container.RegisterType<CartService>().As<ICartService>().InstancePerRequest();

            container.RegisterType<UserLogRepository>().As<IUserLogRepository>().InstancePerRequest();
            container.RegisterType<OrderService>().As<IOrderService>().InstancePerRequest();

            //container.RegisterType<HomeController>();
            // Set the dependency resolver to be Autofac.
            var builder = container.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder));
        }
    }
}