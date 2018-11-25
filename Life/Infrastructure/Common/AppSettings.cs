using System;

namespace Life.Infrastructure.Common
{
	public class AppSettings
	{
		public string[] DirectoriesWithMaps { get; set; }
		public TimeSpan FrameDelay { get; set; }

		public static AppSettings Default = new AppSettings
		{
			DirectoriesWithMaps = new[] { ".", "\\maps" },
			FrameDelay = TimeSpan.FromMilliseconds(50)
		};
	}
}
