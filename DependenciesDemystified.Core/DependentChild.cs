using System;

namespace DependenciesDemystified.Core
{
	public sealed class DependentChild
	{
		private readonly IParent parent;

		public DependentChild(IParent parent)
		{
			if(parent == null)
			{
				throw new ArgumentNullException(nameof(parent));
			}

			this.parent = parent;
		}

		public void DemandFunds()
		{
			this.Wallet += this.parent.ProduceFunds();
		}

		public decimal Wallet { get; private set; }
	}
}
