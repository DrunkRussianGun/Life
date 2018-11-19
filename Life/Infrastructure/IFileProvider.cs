namespace Life.Infrastructure
{
	public interface IFileProvider
	{
		string[] GetFiles(string directory, string searchPattern, bool isLocalPath);
		string[] ReadFile(string fileName);

		string GetFileName(string path);
		string GetDirectoryName(string path);
	}
}
