using Life.GameEngine;
using Life.Infrastructure;
using Life.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Life.Actions
{
	public class GameContinueAction : IUserAction
	{
		public GameContinueAction(
			AppSettings appSettings,
			GameSettings gameSettings,
			IUserInterface userInterface)
		{
			_appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
			_gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
			_userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
		}

		public string Name => "Continue";
		public string Command => "continue";

		public void Perform(CommandContext context)
		{
			if (!_gameSettings.CurrentGame?.InProgress ?? true)
			{
				context.AddError("Cannot continue the finished game");
				return;
			}

			var game = _gameSettings.CurrentGame.Game;
			game.Update();
			OutputMap(game.Map);
		}

		private readonly AppSettings _appSettings;
		private readonly GameSettings _gameSettings;
		private readonly IUserInterface _userInterface;

		private void OutputMap(GameMap map)
		{
			var cells = map.AliveCells;
			var bounds = GetBounds(cells);

			_userInterface.ClearScreen();
			for (var y = bounds.Top; y < bounds.Bottom; ++y)
			{
				for (var x = bounds.Left; x < bounds.Right; ++x)
					_userInterface.Output.Write(cells.Contains(new Point(x, y)) ? '#' : ' ');
				_userInterface.Output.WriteLine();
			}

			Thread.Sleep((int)_appSettings.FrameDelay.TotalMilliseconds);
		}

		private static Rectangle GetBounds(IEnumerable<Point> points)
		{
			var leftX = 0;
			var rightX = 0;
			var topY = 0;
			var bottomY = 0;
			var isFirst = true;

			foreach (var cell in points)
			{
				if (isFirst)
				{
					leftX = rightX = cell.X;
					topY = bottomY = cell.Y;
					isFirst = false;
					continue;
				}

				leftX = Math.Min(leftX, cell.X);
				rightX = Math.Max(rightX, cell.X);
				topY = Math.Min(topY, cell.Y);
				bottomY = Math.Max(bottomY, cell.Y);
			}

			return new Rectangle(leftX, topY, rightX + 1 - leftX, bottomY + 1 - topY);
		}
	}
}
