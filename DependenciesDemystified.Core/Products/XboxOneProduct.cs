namespace DependenciesDemystified.Core.Products
{
	public sealed class XboxOneProduct
		: IProduct
	{
		public XboxOneProduct()
		{
			this.Name = "XBox One";
			this.Cost = 350M;
		}

		public decimal Cost { get; }
		public string Name { get; }
	}
}
