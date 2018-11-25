using System;
using System.IO;

namespace Life.Infrastructure.Default
{
	public class ConsoleUserInterface : IUserInterface
	{
		public TextReader Input => Console.In;
		public TextWriter Output => Console.Out;
		public bool IsInputAvailable => Console.KeyAvailable;

		public void ClearScreen() => Console.Clear();
		public string GetCommand() => Console.ReadLine();
	}
}
