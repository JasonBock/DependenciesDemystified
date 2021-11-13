namespace DependenciesDemystified.Core.Products;

public sealed class SurfaceProduct
	: IProduct
{
	public SurfaceProduct() =>
		(this.Name, this.Cost) = ("Surface", 500M);

	public decimal Cost { get; }
	public string Name { get; }
}