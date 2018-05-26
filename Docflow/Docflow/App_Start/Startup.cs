using Autofac;
using Autofac.Integration.Mvc;
using Docflow.App_Start;
using Docflow.Auth;
using Docflow.Controllers;
using Docflow.Models;
using Docflow.Models.Repositories;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity.Owin;
using Docflow.Models.Listeners;
using NHibernate.Event;
using NHibernate.Dialect;
using Docflow.Models.Autofac;

[assembly: OwinStartup(typeof(Startup))]
namespace Docflow.App_Start
{
    public partial class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var connectionString = ConfigurationManager.ConnectionStrings["MSSQL"];
            if (connectionString == null)
            {
                throw new Exception("Не найдена строка соединения");
            }

            var assembly = Assembly.GetAssembly(typeof(User));
            var builder = new ContainerBuilder();            
            foreach (var type in assembly.GetTypes())
            {
                var attr = (ListenerAttribute)type.GetCustomAttribute(typeof(ListenerAttribute));
                if (attr != null)
                {
                    var interfaces = type.GetInterfaces();
                    var b = builder.RegisterType(type);
                    foreach (var inter in interfaces)
                    {
                        b = b.As(inter);
                    }                  
                }
            }
            builder.Register(x =>
            {
                var cfg = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                        .ConnectionString(connectionString.ConnectionString)
                        .Dialect<MsSql2012Dialect>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                    .ExposeConfiguration(c => {
                        SchemaMetadataUpdater.QuoteTableAndColumns(c);
                        c.EventListeners.PreInsertEventListeners = x.Resolve<IPreInsertEventListener[]>();
                        c.EventListeners.PreUpdateEventListeners = x.Resolve<IPreUpdateEventListener[]>();
                    })
                    .CurrentSessionContext("call");
                var conf = cfg.BuildConfiguration();
                var schemaExport = new SchemaUpdate(conf);
                schemaExport.Execute(true, true);
                return cfg.BuildSessionFactory();
            }).As<ISessionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>()
                .InstancePerRequest();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>()
                .InstancePerDependency();
            builder.RegisterControllers(Assembly.GetAssembly(typeof(AccountController)));
            builder.RegisterModule(new AutofacWebTypesModule());           
            foreach (var type in assembly.GetTypes())
            {
                var attr = type.GetCustomAttribute(typeof(RepositoryAttribute));
                if (attr != null)
                {
                    builder.RegisterType(type);
                }
            }
            var container = builder.Build();

            Locator.SetImpl(new AutofacLocatorImpl(container));
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMiddleware(container);

            app.CreatePerOwinContext(() => 
                new UserManager(new IdentityStore(DependencyResolver.Current.GetServices<ISession>().FirstOrDefault())));
            app.CreatePerOwinContext<SignInManager>((options, context) => 
                new SignInManager(context.GetUserManager<UserManager>(), context.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider()
            });
        }
    }
}