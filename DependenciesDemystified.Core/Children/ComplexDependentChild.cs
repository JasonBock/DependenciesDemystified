using DependenciesDemystified.Core.Logging;
using DependenciesDemystified.Core.Parents;
using DependenciesDemystified.Core.Products;
using System;

namespace DependenciesDemystified.Core.Children
{
	public sealed class ComplexDependentChild
		: IChild
	{
		private readonly Lazy<ILogger> logger;
		private readonly IParent parent;
		private readonly Func<ProductChoices, IProduct> productCreator;
		private readonly Random random;

		public ComplexDependentChild(IParent parent, Lazy<ILogger> logger,
			Func<ProductChoices, IProduct> productCreator, Random random)
		{
			if(parent == null)
			{
				throw new ArgumentNullException(nameof(parent));
			}

			if (logger == null)
			{
				throw new ArgumentNullException(nameof(logger));
			}

			if (productCreator == null)
			{
				throw new ArgumentNullException(nameof(productCreator));
			}

			if (random == null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			this.parent = parent;
			this.logger = logger;
			this.productCreator = productCreator;
			this.random = random;
		}

		public void DemandFunds()
		{
			var funds = this.parent.ProduceFunds();

			if(funds <= 0M)
			{
				this.logger.Value.Log("This isn't fair! I work hard for a living!!");
			}
			else
			{
				this.Wallet += funds;

				var productChoice = (ProductChoices)this.random.Next(0, 3);
				var product = productCreator(productChoice);

				if(this.Wallet > product.Cost)
				{
					this.Wallet -= product.Cost;
					this.logger.Value.Log($"I purchased a {product.Name} valued at {product.Cost}.");
				}
			}
		}

		public decimal Wallet { get; private set; }
	}
}
