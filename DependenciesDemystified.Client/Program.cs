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

//RunHardCodedChild();

static void RunHardCodedChild()
{
	Console.Out.WriteLine(nameof(RunHardCodedChild));
	Console.Out.WriteLine();

	var child = new HardCodedChild();

	for (var i = 0; i < 1000; i++)
	{
		child.DemandFunds();
	}

	Console.Out.WriteLine($"Parent's name is {child.Parent.Name}");
	Console.Out.WriteLine($"Wallet is {child.Wallet:C}");
}

//RunDependentChild();

static void RunDependentChild()
{
	Console.Out.WriteLine(nameof(RunDependentChild));
	Console.Out.WriteLine();

	var childUsesJason = new DependentChild(new JasonAsParent());

	for (var i = 0; i < 1000; i++)
	{
		childUsesJason.DemandFunds();
	}

	Console.Out.WriteLine($"Parent's name is {childUsesJason.Parent.Name}");
	Console.Out.WriteLine($"Wallet is {childUsesJason.Wallet:C}");
	Console.Out.WriteLine();

	using var random = new SecureRandom();
	var childUsesLiz = new DependentChild(new LizAsParent(random));

	for (var i = 0; i < 1000; i++)
	{
		childUsesLiz.DemandFunds();
	}

	Console.Out.WriteLine($"Parent's name is {childUsesLiz.Parent.Name}");
	Console.Out.WriteLine($"Wallet is {childUsesLiz.Wallet:C}");
}

//RunWithServiceCollection();

static void RunWithServiceCollection()
{
	Console.Out.WriteLine(nameof(RunWithServiceCollection));
	Console.Out.WriteLine();

	var services = new ServiceCollection();
	services.AddScoped<IChild, DependentChild>();
	services.AddScoped<IParent, JasonAsParent>();

	using var provider = services.BuildServiceProvider();
	using var scope = provider.CreateScope();
	var child = scope.ServiceProvider.GetService<IChild>()!;

	for (var i = 0; i < 1000; i++)
	{
		child.DemandFunds();
	}

	Console.Out.WriteLine($"Parent's name is {child.Parent.Name}");
	Console.Out.WriteLine($"Wallet is {child.Wallet:C}");
}

//RunWithAutofac();

static void RunWithAutofac()
{
	Console.Out.WriteLine(nameof(RunWithAutofac));
	Console.Out.WriteLine();

	var configuration = new ConfigurationBuilder();
	//configuration.AddJsonFile("autofac.json");

	var builder = new ContainerBuilder();
	builder.RegisterModule<CoreModule>();
	builder.RegisterModule(new ConfigurationModule(configuration.Build()));

	var container = builder.Build();

	using var scope = container.BeginLifetimeScope();
	var child = scope.Resolve<IChild>();

	for (var i = 0; i < 1000; i++)
	{
		child.DemandFunds();
	}

	Console.Out.WriteLine($"Parent's name is {child.Parent.Name}");
	Console.Out.WriteLine($"Wallet is {child.Wallet:C}");
}

//RunWithIntegration();

// https://docs.autofac.org/en/latest/integration/aspnetcore.html
static void RunWithIntegration()
{
	Console.Out.WriteLine(nameof(RunWithIntegration));
	Console.Out.WriteLine();

	var services = new ServiceCollection();

	var builder = new ContainerBuilder();
	builder.Populate(services);
	builder.RegisterModule<CoreModule>();

	var container = builder.Build();
	var provider = new AutofacServiceProvider(container) as IServiceProvider;

	using var scope = provider.CreateScope();
	var child = scope.ServiceProvider.GetService<IChild>()!;

	for (var i = 0; i < 1000; i++)
	{
		child.DemandFunds();
	}

	Console.Out.WriteLine($"Parent's name is {child.Parent.Name}");
	Console.Out.WriteLine($"Wallet is {child.Wallet:C}");
}

RunWithStrongInject();

static void RunWithStrongInject()
{
	Console.Out.WriteLine(nameof(RunWithStrongInject));
	Console.Out.WriteLine();

	using var container = new DependencyContainer();
	container.Run(child =>
	{
		for (var i = 0; i < 1000; i++)
		{
			child.DemandFunds();
		}

		Console.Out.WriteLine($"Parent's name is {child.Parent.Name}");
		Console.Out.WriteLine($"Wallet is {child.Wallet:C}");
	});
}