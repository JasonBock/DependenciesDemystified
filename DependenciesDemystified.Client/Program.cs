using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using DependenciesDemystified;
using DependenciesDemystified.Core;
using DependenciesDemystified.Core.Children;
using DependenciesDemystified.Core.Parents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spackle;
using StrongInject;
using System;

//RunHardCodedChild();
//RunDependentChild();
//RunWithServiceCollection();
//RunWithAutofac();
//RunWithCoreIntegration();
RunWithStrongInject();

static void RunHardCodedChild()
{
	Console.Out.WriteLine(nameof(RunHardCodedChild));

	var child = new HardCodedChild();

	for (var i = 0; i < 1000; i++)
	{
		child.DemandFunds();
	}

	Console.Out.WriteLine(child.Wallet);
	Console.Out.WriteLine(child.Parent.Name);
}

static void RunDependentChild()
{
	Console.Out.WriteLine(nameof(RunDependentChild));

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

static void RunWithServiceCollection()
{
	Console.Out.WriteLine(nameof(RunWithServiceCollection));

	var services = new ServiceCollection();
	services.AddScoped<IChild, DependentChild>();
	services.AddScoped<IParent, JasonAsParent>();

	var provider = services.BuildServiceProvider();

	using var scope = provider.CreateScope();
	var child = scope.ServiceProvider.GetService<IChild>()!;

	for (var i = 0; i < 100; i++)
	{
		child.DemandFunds();
	}

	Console.Out.WriteLine(child.Wallet);
	Console.Out.WriteLine(child.Parent.Name);
}

static void RunWithAutofac()
{
	Console.Out.WriteLine(nameof(RunWithAutofac));

	var config = new ConfigurationBuilder();
	config.AddJsonFile("autofac.json");

	var builder = new ContainerBuilder();
	builder.RegisterModule<CoreModule>();
	builder.RegisterModule(new ConfigurationModule(config.Build()));

	var container = builder.Build();

	using var scope = container.BeginLifetimeScope();
	var child = scope.Resolve<IChild>();

	for (var i = 0; i < 100; i++)
	{
		child.DemandFunds();
	}

	Console.Out.WriteLine(child.Wallet);
	Console.Out.WriteLine(child.Parent.Name);
}

// https://docs.autofac.org/en/latest/integration/aspnetcore.html
static void RunWithCoreIntegration()
{
	Console.Out.WriteLine(nameof(RunWithCoreIntegration));

	var services = new ServiceCollection();

	var builder = new ContainerBuilder();
	builder.Populate(services);
	builder.RegisterModule<CoreModule>();

	var container = builder.Build();
	var provider = new AutofacServiceProvider(container) as IServiceProvider;

	using var scope = provider.CreateScope();
	var child = scope.ServiceProvider.GetService<IChild>()!;

	for (var i = 0; i < 100; i++)
	{
		child.DemandFunds();
	}

	Console.Out.WriteLine(child.Wallet);
	Console.Out.WriteLine(child.Parent.Name);
}

static void RunWithStrongInject()
{
	new DependencyContainer().Run(child =>
	{
		for (var i = 0; i < 1000; i++)
		{
			child.DemandFunds();
		}

		Console.Out.WriteLine($"{nameof(child)} - {child.Wallet}");
	});
}