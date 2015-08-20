namespace DependenciesDemystified.Core.Children
{
	public interface IChild
	{
		void DemandFunds();

		decimal Wallet { get; }
	}
}