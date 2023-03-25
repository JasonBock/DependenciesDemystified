using Spackle;

namespace DependenciesDemystified.Core.Parents;

public sealed class JasonAsParent
	: IParent, IDisposable
{
	private readonly SecureRandom random = new();

	public void Dispose() => 
		((IDisposable)this.random).Dispose();

	public decimal ProduceFunds() =>
		this.random.Next(0, 2) == 0 ? 1000M : 0M;

   public string Name => "Jason";
}