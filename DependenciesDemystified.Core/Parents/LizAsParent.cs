﻿using System;

namespace DependenciesDemystified.Core.Parents
{
	public sealed class LizAsParent
		: IParent
	{
		private readonly Random random;

		public LizAsParent(Random random) => 
			this.random = random ?? throw new ArgumentNullException(nameof(random));

		public decimal ProduceFunds() => 
			this.random.Next(0, 10) == 0 ? 100M : 0M;

		public string Name => "Liz";
	}
}