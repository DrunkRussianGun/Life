using Life.Infrastructure;
using System;

namespace Life.Actions
{
	public class GameEndAction : IUserAction
	{
		public string Name => "End game";
		public string Command => "end";

		public void Perform(CommandContext context)
		{
            if (context == null)
                throw new ArgumentNullException(nameof(context));

			throw new NotImplementedException();
		}
	}
}
