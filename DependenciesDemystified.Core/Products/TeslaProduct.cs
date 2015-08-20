using System;

namespace DependenciesDemystified.Core.Products
{
	public sealed class TeslaProduct
		: IProduct
	{
		public TeslaProduct()
		{
			this.Name = "Tesla";
			this.Cost = 109000M;
		}

		public decimal Cost { get; }
		public string Name { get; }
	}
}
