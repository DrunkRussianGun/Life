namespace Life.Infrastructure
{
	public class CommandContext
	{
		public string Command { get; set; }

		public bool ExitRequested { get; set; }

		public bool Handled { get; set; }
		public string ErrorMessage { get; set; }
	}
}
