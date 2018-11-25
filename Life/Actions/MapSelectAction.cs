using Life.Infrastructure;
using Life.Infrastructure.Common;
using System;

namespace Life.Actions
{
	public class MapSelectAction : IUserAction
	{
		public MapSelectAction(
			GameSettings gameSettings,
			MapParser mapParser,
			MapSelector mapSelector)
		{
			_gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
			_mapParser = mapParser ?? throw new ArgumentNullException(nameof(mapParser));
			_mapSelector = mapSelector ?? throw new ArgumentNullException(nameof(mapSelector));
		}

		public string Name => "Select map";
		public string Command => "select map";

		public void Perform(CommandContext context)
		{
			var map = _mapSelector.Select();
			_gameSettings.StartMap = _mapParser.Parse(map);
		}

		private readonly GameSettings _gameSettings;
		private readonly MapParser _mapParser;
		private readonly MapSelector _mapSelector;
	}
}
