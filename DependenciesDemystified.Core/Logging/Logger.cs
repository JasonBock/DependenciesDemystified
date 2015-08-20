using System;

namespace DependenciesDemystified.Core.Logging
{
	public sealed class Logger
		: ILogger
	{
		public void Log(string message)
		{
			Console.Out.WriteLine($"Logger: {message}");
		}
	}
}
