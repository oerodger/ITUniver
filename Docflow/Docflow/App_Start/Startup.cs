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
            builder.Register(x =>
            {
                var cfg = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                        .ConnectionString(connectionString.ConnectionString)
                        .Dialect<MsSql2012Dialect>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                    .ExposeConfiguration(SchemaMetadataUpdater.QuoteTableAndColumns)
                    .CurrentSessionContext("call");
                var conf = cfg.BuildConfiguration();
                var preInsertEventListeners = new List<IPreInsertEventListener>();
                foreach (var type in assembly.GetTypes())
                {
                    var attr = (ListenerAttribute)type.GetCustomAttribute(typeof(ListenerAttribute));
                    if (attr != null)
                    {
                        if (attr.ListenerType == Models.Listeners.ListenerType.PreInsert)
                        {
                            preInsertEventListeners.Add((IPreInsertEventListener)Activator.CreateInstance(type));
                        }                       
                    }
                }
                conf.EventListeners.PreInsertEventListeners = preInsertEventListeners.ToArray();
               var schemaExport = new SchemaUpdate(conf);
                schemaExport.Execute(true, true);
                return cfg.BuildSessionFactory();
            }).As<ISessionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>()
                .InstancePerRequest();
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