﻿using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Ddd.Template.Contracts.Commands;
using Ddd.Template.Web.Scaffolding;
using Ddd.Template.Web.Scaffolding.Configuration;
using NServiceBus;

namespace Ddd.Template.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private static IWindsorContainer Container;

		protected void Application_Start()
		{
			BootstrapContainer();

			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();

			Container
				.Resolve<IBus>()
				.Send(new AddDocument { Id = Guid.NewGuid().ToString(), UserId = Guid.NewGuid().ToString() });
		}

		protected void Application_End()
		{
			Container.Dispose();
		}

		private static void BootstrapContainer()
		{
			Container = new WindsorContainer().Install(FromAssembly.This());

			// Let windsor handle all our IoC, things'll just work! :]
			//DependencyResolver.SetResolver(new WindsorDependepcyResolver(_container));
			var controllerFactory = new WindsorControllerFactory(Container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
		}
	}
}
	