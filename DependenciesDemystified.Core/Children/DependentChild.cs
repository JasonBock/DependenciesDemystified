using DependenciesDemystified.Core.Parents;
using System;

namespace DependenciesDemystified.Core.Children
{
	public sealed class DependentChild
		: IChild
	{
		public DependentChild(IParent parent) => 
			this.Parent = parent ?? throw new ArgumentNullException(nameof(parent));

		public void DemandFunds() => this.Wallet += this.Parent.ProduceFunds();

		public decimal Wallet { get; private set; }

		public IParent Parent { get; }
	}
}