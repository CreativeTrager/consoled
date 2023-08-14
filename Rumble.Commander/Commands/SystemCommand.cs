using System;

namespace Rumble.Commander.Commands;

///
/// <inheritdoc />
///
public partial class Command
{
	/// <summary>
	/// System command in its default state.
	/// </summary>
	public static partial class System
	{
		/// <summary>
		/// Quit system command in its default state.
		/// </summary>
		/// <param name="action">Action to be executed  by the command.</param>
		/// <returns>Instance of the command.</returns>
		internal static Command Quit(Action action) => new ()
		{
			Action = action,
			Settings = new ()
			{
				Name = System.Name.Quit.ToString(),
				Aliases = new () { "exit", "q" }
			}
		};

		/// <summary>
		/// Help system command in its default state.
		/// </summary>
		/// <param name="action">Action to be executed  by the command.</param>
		/// <returns>Instance of the command.</returns>
		internal static Command Help(Action action) => new ()
		{
			Action = action,
			Settings = new ()
			{
				Name = System.Name.Help.ToString(),
				Aliases = new () { "about", "h" },
				AskForConfirmation = false
			}
		};
	}
}