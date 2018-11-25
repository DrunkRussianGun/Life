using Life.Infrastructure;
using Life.Infrastructure.Common;
using System;

namespace Life
{
	public class ActionPerformer
	{
		public CommandContext Perform(IUserAction action)
		{
			if (action == null)
				throw new ArgumentNullException(nameof(action));

			var context = new CommandContext
			{
				Command = action.Name
			};

			try
			{
				action.Perform(context);
			}
			catch (Exception error)
			{
				context.AddError(error.ToString());
			}

			return context;
		}
	}
}
