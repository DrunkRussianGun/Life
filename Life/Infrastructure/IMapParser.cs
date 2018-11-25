using Life.GameEngine;

namespace Life.Infrastructure
{
	public interface IMapParser
	{
		GameMap Parse(string[] lines);
	}
}
