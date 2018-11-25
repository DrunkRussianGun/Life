using Life.GameEngine;
using Life.Infrastructure;
using Life.Infrastructure.Common;
using System;

namespace Life.Actions
{
	public class GameStartAction : IUserAction
	{
		public string Description => "Start a new game";
		public string Name => "Start game";
		public string Command => "start";

		public GameStartAction(GameSettings gameSettings)
		{
			_gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
		}

		public void Perform(CommandContext context)
		{
			if (_gameSettings.StartMap == null)
			{
				context.AddError("Select map first");
				return;
			}

			_gameSettings.CurrentGame = new GameContext
			{
				Game = new Game(_gameSettings.StartMap),
				InProgress = true
			};
		}
		
		private readonly GameSettings _gameSettings;
	}
}
