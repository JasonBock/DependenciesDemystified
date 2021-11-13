using DependenciesDemystified.Core.Children;
using DependenciesDemystified.Core.Parents;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Rocks;

namespace DependenciesDemystified.Core.Tests.Children;

public static class DependentChildTests
{
	// This works, but it creates a container, which is unnecessary.
	[Test]
	public static void DemandFundsWithAContainer()
	{
		var parent = Rock.Create<IParent>();
		parent.Methods().ProduceFunds().Returns(5);

		var container = new ServiceCollection();
		container.AddTransient<IChild, DependentChild>();
		container.AddSingleton(parent.Instance());

		var provider = container.BuildServiceProvider();

		using (var scope = provider.CreateScope())
		{
			var child = scope.ServiceProvider.GetService<IChild>();
			child!.DemandFunds();

			Assert.That(child.Wallet, Is.EqualTo(5));
		}

		parent.Verify();
	}

	// This works, and doesn't involve a container (the preferred way).
	[Test]
	public static void DemandFundsWithoutAContainer()
	{
		var parent = Rock.Create<IParent>();
		parent.Methods().ProduceFunds().Returns(5);

		var child = new DependentChild(parent.Instance());
		child.DemandFunds();

		Assert.That(child.Wallet, Is.EqualTo(5));

		parent.Verify();
	}
}