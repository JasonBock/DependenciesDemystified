namespace DependenciesDemystified.Core.Products
{
	public sealed class XboxOneProduct
		: IProduct
	{
		public XboxOneProduct() => (this.Name, this.Cost) = ("XBox One", 350M);

		public decimal Cost { get; }
		public string Name { get; }
	}
}
