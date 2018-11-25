using Life.Actions;
using Life.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Life
{
	public class ActionSelector
	{
		public ActionSelector(
			IUserAction[] actions,
			IUserInterface userInterface,
			Func<GameContext> gameSettingsProvider)
		{
			_actions = actions?.ToDictionary(action => action.Command)
				?? throw new ArgumentNullException(nameof(actions));
			_gameSettingsProvider = gameSettingsProvider
				?? throw new ArgumentNullException(nameof(gameSettingsProvider));
			_userInterface = userInterface
				?? throw new ArgumentNullException(nameof(userInterface));
		}

		public IUserAction GetNextAction()
		{
			if (_userInterface.IsInputAvailable)
			{
				var action = AskActionFromUserOnce();
				if (action != null)
					return action;
			}

			if (_gameSettingsProvider()?.InProgress ?? false)
				return GetAction<GameContinueAction>();

			return AskActionFromUser();
		}

		private IUserAction AskActionFromUser()
		{
			IUserAction action;
			do
				action = AskActionFromUserOnce();
			while (action == null);
			return action;
		}

		private IUserAction AskActionFromUserOnce()
		{
			var command = _userInterface.GetCommand();
			if (!_actions.TryGetValue(command, out var action))
			{
				_userInterface.Output.WriteLine("Unknown command");
				return null;
			}

			return action;
		}

		private IUserAction GetAction<T>()
			where T : IUserAction
		{
			return _actions.Values.Single(action => action is T);
		}

		private readonly IReadOnlyDictionary<string, IUserAction> _actions;
		private readonly Func<GameContext> _gameSettingsProvider;
		private readonly IUserInterface _userInterface;
	}
}
