namespace DependenciesDemystified.Core
{
	public sealed class HardCodedChild
	{
		public void DemandFunds()
		{
			var parent = new JasonAsParent();
			this.Wallet += parent.ProduceFunds();
		}

		public decimal Wallet { get; private set; }
	}
}
