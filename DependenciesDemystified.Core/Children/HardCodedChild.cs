using DependenciesDemystified.Core.Parents;

namespace DependenciesDemystified.Core.Children
{
	public sealed class HardCodedChild 
		: IChild
	{
		public HardCodedChild() => this.Parent = new JasonAsParent();

		public void DemandFunds() => this.Wallet += this.Parent.ProduceFunds();

		public decimal Wallet { get; private set; }

		public IParent Parent { get; }
	}
}
