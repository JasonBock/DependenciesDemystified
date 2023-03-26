namespace DependenciesDemystified.Core.Products;

public sealed class DodgeViperProduct
	: IProduct
{
	public DodgeViperProduct() =>
		(this.Name, this.Cost) = ("Dodge Viper", 109000M);

	public decimal Cost { get; }
	public string Name { get; }
}