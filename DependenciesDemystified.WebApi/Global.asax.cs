using Autofac;
using Autofac.Integration.WebApi;
using DependenciesDemystified.Core;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace DependenciesDemystified.WebApi
{
	// Lifted from: http://autofac.readthedocs.org/en/latest/integration/webapi.html
	public class WebApiApplication : 
		HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

			var builder = new ContainerBuilder();

			// Get your HttpConfiguration.
			var config = GlobalConfiguration.Configuration;

			// Register your Web API controllers.
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
			builder.RegisterModule<CoreModule>();

			// OPTIONAL: Register the Autofac filter provider.
			builder.RegisterWebApiFilterProvider(config);

			// Set the dependency resolver to be Autofac.
			var container = builder.Build();
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}
	}
}
