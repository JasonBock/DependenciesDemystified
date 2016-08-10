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

			var configuration = GlobalConfiguration.Configuration;

			var builder = new ContainerBuilder();
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
			builder.RegisterModule<CoreModule>();
			builder.RegisterWebApiFilterProvider(configuration);

			var container = builder.Build();
			configuration.DependencyResolver =
				new AutofacWebApiDependencyResolver(container);
		}
	}
}
