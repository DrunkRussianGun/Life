using System.Collections.Generic;

namespace Life.Infrastructure
{
	public class CommandContext
	{
		public string Command { get; set; }

		public bool ExitRequested { get; set; }

		public bool Handled { get; private set; } = true;
		public List<string> ErrorMessages { get; } = new List<string>();

		public void AddError(string message)
		{
			Handled = false;
			ErrorMessages.Add(message);
		}
	}
}
