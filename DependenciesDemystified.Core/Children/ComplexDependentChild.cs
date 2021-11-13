using DependenciesDemystified.Core.Logging;
using DependenciesDemystified.Core.Parents;
using DependenciesDemystified.Core.Products;
using Spackle;

namespace DependenciesDemystified.Core.Children;

public sealed class ComplexDependentChild
	: IChild
{
	private readonly Lazy<ILogger> logger;
	private readonly Func<ProductChoices, IProduct> productCreator;
	private readonly SecureRandom random;

	public ComplexDependentChild(IParent parent, Lazy<ILogger> logger,
		Func<ProductChoices, IProduct> productCreator, SecureRandom random)
	{
		this.Parent = parent ?? throw new ArgumentNullException(nameof(parent));
		this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		this.productCreator = productCreator ?? throw new ArgumentNullException(nameof(productCreator));
		this.random = random ?? throw new ArgumentNullException(nameof(random));
	}

	public void DemandFunds()
	{
		var funds = this.Parent.ProduceFunds();

		if (funds <= 0M)
		{
			this.logger.Value.Log("This isn't fair! I work hard for a living!!");
		}
		else
		{
			this.Wallet += funds;

			var productChoice = (ProductChoices)this.random.Next(0, 3);
			var product = this.productCreator(productChoice);

			if (this.Wallet > product.Cost)
			{
				this.Wallet -= product.Cost;
				this.logger.Value.Log($"I purchased a {product.Name} valued at {product.Cost}.");
			}
		}
	}

	public decimal Wallet { get; private set; }

	public IParent Parent { get; }
}