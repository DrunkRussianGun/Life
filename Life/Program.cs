using Life.Infrastructure;
using Life.Infrastructure.Common;
using Life.Infrastructure.Default;
using Ninject;
using Ninject.Extensions.Conventions;
using System;

namespace Life
{
	internal class Program
	{
		private static readonly StandardKernel Container;

		static Program()
		{
			Container = new StandardKernel();

			Container.Bind<IFileProvider>().To<DefaultFileProvider>().InSingletonScope();
			Container.Bind<IUserInterface>().To<ConsoleUserInterface>().InSingletonScope();

			Container.Bind<AppSettings>().ToMethod(
				context => AppSettings.Default).InSingletonScope();
			Container.Bind<GameSettings>().ToSelf().InSingletonScope();
			Container.Bind<GameContext>().ToMethod(
				context => context.Kernel.Get<GameSettings>().CurrentGame);
			
			Container.Bind(kernel => kernel
				.FromThisAssembly()
				.SelectAllClasses()
				.InheritedFrom<IUserAction>()
				.BindAllInterfaces());
		}

		private static void Main(string[] args)
		{
			Console.Title = "Life";

			var actionSelector = Container.Get<ActionSelector>();
			var actionPerformer = Container.Get<ActionPerformer>();
			while (true)
			{
				var action = actionSelector.GetNextAction();
				var commandContext = actionPerformer.Perform(action);

				if (!commandContext.Handled)
					Console.WriteLine(
						string.Join("\r\n", commandContext.ErrorMessages));
				if (commandContext.ExitRequested)
					return;
			}
		}
	}
}
