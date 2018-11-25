using Life.Infrastructure;
using System;

namespace Life.Actions
{
	public class GameStartAction : IUserAction
	{
		public GameStartAction(GameSettings gameSettings)
		{
			_gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
		}

		public string Name => "Start game";
		public string Command => "start";

		public void Perform(CommandContext context)
		{
			_gameSettings.CurrentGame = new GameContext
			{
				Game = new Game(_gameSettings.StartMap),
				InProgress = true
			};
		}
		
		private readonly GameSettings _gameSettings;
	}
}
