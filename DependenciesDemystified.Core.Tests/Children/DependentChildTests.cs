using Autofac;
using DependenciesDemystified.Core.Children;
using DependenciesDemystified.Core.Parents;
using Moq;
using NUnit.Framework;

namespace DependenciesDemystified.Core.Tests.Children
{
	[TestFixture]
	public static class DependentChildTests
	{
		// This works, but it creates a container, which is unnecessary.
		[Test]
		public static void DemandFundsWithAContainer()
		{
			var parent = new Mock<IParent>(MockBehavior.Strict);
			parent.Setup(_ => _.ProduceFunds()).Returns(5);

			var container = new ContainerBuilder();
			container.RegisterInstance(parent.Object).As<IParent>();
			container.RegisterType<DependentChild>().As<IChild>();

			using (var scope = container.Build().BeginLifetimeScope())
			{
				var child = scope.Resolve<IChild>();
				child.DemandFunds();

				Assert.That(child.Wallet, Is.EqualTo(5));
			}

			parent.VerifyAll();
		}

		// This works, and doesn't involve a container (the preferred way).
		[Test]
		public static void DemandFundsWithoutAContainer()
		{
			var parent = new Mock<IParent>(MockBehavior.Strict);
			parent.Setup(_ => _.ProduceFunds()).Returns(5);

			var child = new DependentChild(parent.Object);
			child.DemandFunds();

			Assert.That(child.Wallet, Is.EqualTo(5));

			parent.VerifyAll();
		}
	}
}