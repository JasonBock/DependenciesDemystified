using Spackle;
using System;

namespace DependenciesDemystified.Core.Parents
{
	public sealed class LizAsParent
		: IParent
	{
		private readonly Random random;

		public LizAsParent(Random random)
		{
			if(random == null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			this.random = random;
		}

		public decimal ProduceFunds()
		{
			if (this.random.Next(0, 10) == 0)
			{
				return 100M;
			}
			else
			{
				return 0M;
			}
		}

		public string Name => "Liz";
	}
}