namespace Life.Infrastructure
{
	public class GameSettings
	{
		public GameMap StartMap { get; set; }

		public GameContext CurrentGame { get; set; } = new GameContext
		{
			InProgress = false
		};
	}
}
