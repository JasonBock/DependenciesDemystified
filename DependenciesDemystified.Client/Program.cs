using DependenciesDemystified.Core;
using System;

namespace DependenciesDemystified.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			//Program.RunHardCodedChild();
			Program.RunDependentChild();
		}

		private static void RunDependentChild()
		{
			var childUsesJason = new DependentChild(new JasonAsParent());

			for (var i = 0; i < 1000; i++)
			{
				childUsesJason.DemandFunds();
			}

			Console.Out.WriteLine($"{nameof(childUsesJason)} - {childUsesJason.Wallet}");

			var childUsesLiz = new DependentChild(new LizAsParent());

			for (var i = 0; i < 1000; i++)
			{
				childUsesLiz.DemandFunds();
			}

			Console.Out.WriteLine($"{nameof(childUsesLiz)} - {childUsesLiz.Wallet}");
		}

		private static void RunHardCodedChild()
		{
			var child = new HardCodedChild();

			for(var i = 0; i < 1000; i++)
			{
				child.DemandFunds();
			}

			Console.Out.WriteLine(child.Wallet);
		}
	}
}
