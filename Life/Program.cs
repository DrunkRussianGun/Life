using Life.Actions;
using Life.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace Life
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Console.Title = "Life";

			var commandHandler = new CommandHandler(new[] {
				new MapSelectAction(
					new MapSelector(new[] { ".", "\\maps" }, new DefaultFileProvider(), new ConsoleUserInterface()),
					new MapParser(),
					new GameSettings()
				) }
			);
			var ui = new ConsoleUserInterface();
			while (true)
			{
				var command = ui.GetCommand();
				var context = commandHandler.Handle(command);

				if (context.ExitRequested)
					return;
				if (!context.Handled)
					Console.WriteLine(context.ErrorMessage);
			}
		}
	}
}
