using Life.Infrastructure;
using System;

namespace Life.Actions
{
	public class MapSelectAction : IUserAction
	{
		public MapSelectAction(
			MapSelector fileSelector,
			IMapParser mapParser,
			GameSettings gameSettings)
		{
			_mapSelector = fileSelector ?? throw new ArgumentNullException(nameof(fileSelector));
			_gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
			_mapParser = mapParser ?? throw new ArgumentNullException(nameof(mapParser));
		}

		public string Name => "Select map";
		public string Command => "select map";

		public void Perform(CommandContext context)
		{
			var map = _mapSelector.Select();
			_gameSettings.StartMap = _mapParser.Parse(map);
		}

		private readonly MapSelector _mapSelector;
		private readonly GameSettings _gameSettings;
		private readonly IMapParser _mapParser;
	}
}
