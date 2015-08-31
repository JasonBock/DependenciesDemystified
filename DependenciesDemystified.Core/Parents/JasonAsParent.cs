using Spackle;

namespace DependenciesDemystified.Core.Parents
{
	public sealed class JasonAsParent
		: IParent
	{
		private readonly SecureRandom random = new SecureRandom();

		public decimal ProduceFunds()
		{
			if (this.random.Next(0, 2) == 0)
			{
				return 1000M;
			}
			else
			{
				return 0M;
			}
		}

		public string Name { get { return "Jason"; } }
	}
}
