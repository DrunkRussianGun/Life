using Life.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Life
{
	public class MapParser : IMapParser
	{
		public GameMap Parse(string[] lines)
		{
			IEnumerable<string> mapLines = lines;

			var bounds = ParseBounds(lines);
			if (bounds != null)
				mapLines = lines.Skip(2);
			var map = ParseMap(mapLines);

			return new GameMap(map, bounds);
		}

		private IEnumerable<Point> ParseMap(IEnumerable<string> lines)
		{
			return lines.SelectMany((line, y) =>
					line.Select((ch, x) =>
						ch == ' ' ? null : new Point?(new Point(x, y))))
				.Where(point => point != null)
				.Select(point => point.Value);
		}

		private Rectangle? ParseBounds(string[] lines)
		{
			if (lines.Length < 2 || lines[1] != "")
				return null;

			var sizes = lines[0]
				.Split(" xX".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
				.Select(x => int.Parse(x))
				.ToArray();
			return new Rectangle(0, 0, sizes[0], sizes[1]);
		}
	}
}
