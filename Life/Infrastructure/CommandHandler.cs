using System.Collections.Generic;
using System.Linq;

namespace Life.Infrastructure
{
	public class CommandHandler
	{
		public CommandHandler(IEnumerable<IUserAction> actions)
		{
			_actions = actions?.ToDictionary(action => action.Command)
				 ?? new Dictionary<string, IUserAction>();
		}

		public CommandContext Handle(string command)
		{
			var context = new CommandContext
			{
				Command = command,
				Handled = true
			};

			if (_actions.TryGetValue(command, out var action))
				action.Perform(context);
			else
			{
				context.Handled = false;
				context.ErrorMessage = "Unknown command";
			}

			return context;
		}

		private readonly IReadOnlyDictionary<string, IUserAction> _actions;
	}
}
