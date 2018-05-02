using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using DependenciesDemystified.Core;
using DependenciesDemystified.Core.Children;
using DependenciesDemystified.Core.Parents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spackle;
using System;

namespace DependenciesDemystified.Client
{
	class Program
	{
		//Program.RunHardCodedChild();
		//Program.RunDependentChild();
		//Program.RunWithSimpleContainer();
		//Program.RunWithAutofac();
		//Program.RunWithCoreIntegration();
		static void Main(string[] args) =>
			Program.RunWithCoreIntegration();

		private static void RunHardCodedChild()
		{
			Console.Out.WriteLine(nameof(Program.RunHardCodedChild));

			var child = new HardCodedChild();

			for (var i = 0; i < 1000; i++)
			{
				child.DemandFunds();
			}

			Console.Out.WriteLine(child.Wallet);
			Console.Out.WriteLine(child.Parent.Name);
		}

		private static void RunDependentChild()
		{
			Console.Out.WriteLine(nameof(Program.RunDependentChild));

			var childUsesJason = new DependentChild(new JasonAsParent());

			for (var i = 0; i < 1000; i++)
			{
				childUsesJason.DemandFunds();
			}

			Console.Out.WriteLine($"{nameof(childUsesJason)} - {childUsesJason.Wallet}");

			var childUsesLiz = new DependentChild(new LizAsParent(new SecureRandom()));

			for (var i = 0; i < 1000; i++)
			{
				childUsesLiz.DemandFunds();
			}

			Console.Out.WriteLine($"{nameof(childUsesLiz)} - {childUsesLiz.Wallet}");
		}

		private static void RunWithSimpleContainer()
		{
			Console.Out.WriteLine(nameof(Program.RunWithSimpleContainer));

			var container = new SimpleContainer();
			container.Register<IParent>(c => new JasonAsParent());
			container.Register<IChild>(c => new DependentChild(c.Resolve<IParent>()));

			var child = container.Resolve<IChild>();

			for (var i = 0; i < 1000; i++)
			{
				child.DemandFunds();
			}

			Console.Out.WriteLine(child.Wallet);
		}

		private static void RunWithAutofac()
		{
			Console.Out.WriteLine(nameof(Program.RunWithAutofac));

			var config = new ConfigurationBuilder();
			config.AddJsonFile("autofac.json");

			var builder = new ContainerBuilder();
			builder.RegisterModule<CoreModule>();
			builder.RegisterModule(new ConfigurationModule(config.Build()));

			var container = builder.Build();

			using (var scope = container.BeginLifetimeScope())
			{
				var child = scope.Resolve<IChild>();

				for (var i = 0; i < 100; i++)
				{
					child.DemandFunds();
				}

				Console.Out.WriteLine(child.Wallet);
				Console.Out.WriteLine(child.Parent.Name);
			}
		}

		private static void RunWithCoreIntegration()
		{
			var services = new ServiceCollection();

			var builder = new ContainerBuilder();
			builder.Populate(services);
			builder.RegisterModule<CoreModule>();

			var container = builder.Build();
			var provider = new AutofacServiceProvider(container);

			using (var scope = provider.CreateScope())
			{
				var child = scope.ServiceProvider.GetService<IChild>();

				for (var i = 0; i < 100; i++)
				{
					child.DemandFunds();
				}

				Console.Out.WriteLine(child.Wallet);
				Console.Out.WriteLine(child.Parent.Name);
			}
		}
	}
}