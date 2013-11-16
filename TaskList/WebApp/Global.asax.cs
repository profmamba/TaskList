using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using ProfMamba.TaskList.Context;
using ProfMamba.TaskList.Interfaces;
using ProfMamba.TaskList.Logic;
using ProfMamba.TaskList.WebApp.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProfMamba.TaskList.WebApp
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			//Autofac IOC
			var builder = new ContainerBuilder();
			builder.RegisterControllers(Assembly.GetExecutingAssembly());
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
			builder.RegisterType<TaskListContext>().As<ITaskListContext>();
			builder.RegisterType<TaskListContextFactory>().As<ITaskListContextFactory>();
			builder.RegisterType<TaskListLogic>().As<ITaskListLogic>();

			IContainer container = builder.Build();
			var resolver = new AutofacWebApiDependencyResolver(container);
			GlobalConfiguration.Configuration.DependencyResolver = resolver;

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();
		}
	}
}