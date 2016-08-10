namespace DependenciesDemystified.Core.Parents
{
	public sealed class GrandParent
		: IParent
	{
		public decimal ProduceFunds()
		{
			return 10000M;
		}

		public string Name => "Jack";
	}
}
