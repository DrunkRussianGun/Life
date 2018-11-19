using Life.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Life.Actions
{
	public class GameRunner
	{
		public GameRunner(
			AppSettings appSettings,
			IUserInterface userInterface,
			Action commandHandler)
		{
			_appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
			_userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
			_commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
		}

		public void Run(GameContext context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));
			
			OutputMap(context);
			while (context.InProgress)
			{
				context.Game.Update();
				
				OutputMap(context);

				if (_userInterface.IsInputAvailable)
					_commandHandler();
			}
		}

		private readonly AppSettings _appSettings;
		private readonly IUserInterface _userInterface;
		private readonly Action _commandHandler;

		private void OutputMap(GameContext context)
		{
			var map = context.Game.Map.AliveCells;
			var bounds = GetBounds(map);

			_userInterface.ClearScreen();
			for (var y = bounds.Top; y < bounds.Bottom; ++y)
			{
				for (var x = bounds.Left; x < bounds.Right; ++x)
					_userInterface.Output.Write(map.Contains(new Point(x, y)) ? '#' : ' ');
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
