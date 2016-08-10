using Autofac;
using DependenciesDemystified.Core.Children;
using DependenciesDemystified.Core.Parents;
using FluentAssertions;
using Moq;
using Xunit;

namespace DependenciesDemystified.Core.Tests.Children
{
	public sealed class DependentChildTests
	{
		// This works, but it creates a container, which is unnecessary.
		[Fact]
		public void DemandFundsWithAContainer()
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

				child.Wallet.Should().Be(5);
			}

			parent.VerifyAll();
		}

		// This works, and doesn't involve a container (the preferred way).
		[Fact]
		public void DemandFundsWithoutAContainer()
		{
			var parent = new Mock<IParent>(MockBehavior.Strict);
			parent.Setup(_ => _.ProduceFunds()).Returns(5);

			var child = new DependentChild(parent.Object);
			child.DemandFunds();

			child.Wallet.Should().Be(5);

			parent.VerifyAll();
		}
	}
}
