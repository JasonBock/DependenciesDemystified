using DependenciesDemystified.Core.Parents;
using System;

namespace DependenciesDemystified.Core.Children
{
	public sealed class DependentChild
		: IChild
	{
		private readonly IParent parent;

		public DependentChild(IParent parent) => 
			this.parent = parent ?? throw new ArgumentNullException(nameof(parent));

		public void DemandFunds() => this.Wallet += this.parent.ProduceFunds();

		public decimal Wallet { get; private set; }

		public IParent Parent => this.parent;
	}
}
