using System;

namespace DependenciesDemystified.Core.Logging
{
	public sealed class Logger
		: ILogger, IDisposable
	{
		public void Log(string message)
		{
			Console.Out.WriteLine($"Logger: {message}");
		}

		public void Dispose()
		{
			Console.Out.WriteLine("Logger is disposed.");
		}
	}
}
