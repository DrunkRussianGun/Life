using System.IO;

namespace Life.Infrastructure
{
	public class DefaultFileProvider : IFileProvider
	{
		public string GetDirectoryName(string path) =>
			Path.GetDirectoryName(path);
		public string GetFileName(string path) =>
			Path.GetFileName(path);
		public string[] GetFiles(string directory, string searchPattern, bool isLocalPath = true) =>
			Directory.GetFiles(
				(isLocalPath ? Directory.GetCurrentDirectory() : "") + directory,
				searchPattern);
		public string[] ReadFile(string fileName) =>
			File.ReadAllLines(fileName);
	}
}
