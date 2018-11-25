using System.Collections.Generic;
using System.Drawing;

namespace Life.Infrastructure
{
	public class GameMap
	{
		public HashSet<Point> AliveCells { get; }
		public Rectangle? Bounds { get; }

		public GameMap(params Point[] cells) : this(cells, null) { }

		public GameMap(IEnumerable<Point> aliveCells, Rectangle? bounds = null)
		{
			AliveCells = new HashSet<Point>(aliveCells);
			Bounds = bounds;
		}
	}
}
