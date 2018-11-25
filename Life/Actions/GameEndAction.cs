using Life.Infrastructure;
using System;

namespace Life.Actions
{
	public class GameEndAction : IUserAction
	{
		public GameEndAction(GameSettings gameSettings)
		{
			_gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
		}

		public string Name => "End game";
		public string Command => "end";

		public void Perform(CommandContext context)
		{
			_gameSettings.CurrentGame.InProgress = false;
		}

		private readonly GameSettings _gameSettings;
	}
}
