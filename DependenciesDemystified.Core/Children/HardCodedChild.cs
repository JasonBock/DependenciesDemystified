using DependenciesDemystified.Core.Parents;

namespace DependenciesDemystified.Core.Children
{
	public sealed class HardCodedChild 
		: IChild
	{
		public void DemandFunds()
		{
			var parent = new JasonAsParent();
			this.Wallet += parent.ProduceFunds();
		}

		public decimal Wallet { get; private set; }
	}
}
