using Life.Infrastructure.Common;

namespace Life.Infrastructure
{
	public interface IUserAction
	{
		string Name { get; }
		string Command { get; }

		void Perform(CommandContext context);
	}
}
