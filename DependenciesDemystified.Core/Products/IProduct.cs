namespace DependenciesDemystified.Core.Products;

public interface IProduct
{
	decimal Cost { get; }
	string Name { get; }
}