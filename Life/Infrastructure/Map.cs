using System.Collections.Generic;
using System.Drawing;

namespace Life.Infrastructure
{
	public class Map
	{
		public HashSet<Point> AliveCells { get; }
		public Rectangle? Bounds { get; }

		public Map(params Point[] cells) : this(cells, null) { }

		public Map(IEnumerable<Point> aliveCells, Rectangle? bounds = null)
		{
			AliveCells = new HashSet<Point>(aliveCells);
			Bounds = bounds;
		}
	}
}
