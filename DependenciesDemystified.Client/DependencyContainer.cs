using DependenciesDemystified.Core.Children;
using DependenciesDemystified.Core.Parents;
using StrongInject;

namespace DependenciesDemystified
{
	[Register(typeof(DependentChild), typeof(IChild))]
	[Register(typeof(JasonAsParent), typeof(IParent))]
	public partial class DependencyContainer 
		: IContainer<IChild> { }
}