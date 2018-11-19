using Life.Infrastructure;
using System;

namespace Life.Actions
{
	public class GameStartAction : IUserAction
	{
		public GameStartAction(
			GameSettings gameSettings,
			GameRunner gameRunner)
		{
			_gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
			_gameRunner = gameRunner ?? throw new ArgumentNullException(nameof(gameRunner));
		}

		public string Name => "Start game";
		public string Command => "start";

        public void Perform(CommandContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var gameContext = new GameContext
			{
				Game = new Game(_gameSettings.StartMap),
				InProgress = true
			};
			_gameRunner.Run(gameContext);
		}

		private readonly GameSettings _gameSettings;
		private readonly GameRunner _gameRunner;
	}
}
