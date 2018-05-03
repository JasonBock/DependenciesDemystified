namespace DependenciesDemystified.Core.Parents
{
	public sealed class GrandParent
		: IParent
	{
		public decimal ProduceFunds() => 10000M;

		public string Name => "Jack";
	}
}
