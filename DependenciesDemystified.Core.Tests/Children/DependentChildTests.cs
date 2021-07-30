using Autofac;
using DependenciesDemystified.Core.Children;
using DependenciesDemystified.Core.Parents;
using NUnit.Framework;
using Rocks;

namespace DependenciesDemystified.Core.Tests.Children
{
	public static class DependentChildTests
	{
		// This works, but it creates a container, which is unnecessary.
		[Test]
		public static void DemandFundsWithAContainer()
		{
			var parent = Rock.Create<IParent>();
			parent.Methods().ProduceFunds().Returns(5);

			var container = new ContainerBuilder();
			container.RegisterInstance(parent.Instance()).As<IParent>();
			container.RegisterType<DependentChild>().As<IChild>();

			using (var scope = container.Build().BeginLifetimeScope())
			{
				var child = scope.Resolve<IChild>();
				child.DemandFunds();

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
}