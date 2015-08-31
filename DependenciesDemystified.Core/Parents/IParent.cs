namespace DependenciesDemystified.Core.Parents
{
	public interface IParent
	{
		decimal ProduceFunds();
		string Name { get; }
	}
}
