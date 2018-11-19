using System.IO;

namespace Life.Infrastructure
{
	public interface IUserInterface
	{
		TextReader Input { get; }
		TextWriter Output { get; }
		bool IsInputAvailable { get; }

		void ClearScreen();
		string GetCommand();
	}
}
