using Rumble.Commander.Commanders;
using Rumble.Commander.Commands;
using Rumble.Commander.Demo.Runnable;

// There are 2 different commanders for now: CommonCommander and ObjectCommander.
// Here is the usage example of one of the commanders – ObjectCommander.

using var commonCommander = new ObjectCommander<DummyServer>(new DummyServer())
{
	Settings = new ()
	{
		// Default values can be overridden...
		InputPrompt = "Please, enter the command",

		// ...or left as they are (can be omitted).
		ConfirmationPrompt = default!,
		UnknownInputHint = default!,

		// Extra tweaks also have default values
		// but available for override even for specific command.
		UseAliases = true,
		MatchCase = true,
		AskForConfirmation = false
	},
	CustomCommands = new ()
	{
		new ()
		{
			Action = commandable => commandable.Initialize(),
			Settings = new ()
			{
				Name = "initialize",
				Aliases = new () { "init" },

				// Commander will NOT use the aliases of this specific command.
				UseAliases = false
			}
		},
		new ()
		{
			Action = commandable => commandable.Start(),
			Settings = new ()
			{
				Name = "start",
				Aliases = new () { "begin", "run" },

				// Commander WILL treat "Start", "rUn" etc. as valid command names.
				MatchCase = false
			}
		},
		new ()
		{
			Action = commandable => commandable.Stop(),
			Settings = new ()
			{
				Name = "stop",
				Aliases = new () { "terminate" },

				// Commander WILL ask for confirmation.
				// This overrides behaviour specified on Commander level.
				AskForConfirmation = true,
				ConfirmationPrompt = "This command will STOP the server. Are you sure?"
			}
		}
	},

	// System commands such as "help" or "quit" can be overridden.
	// All parameters are available for override EXCEPT Action and Name.
	SystemCommandsOverrides = new ()
	{
		[Command.System.Name.Quit] = new ()
		{
			Aliases = new () { "release", "exit", "qq" },
			ConfirmationPrompt = "You are about to QUIT the current commander. Do you confirm?"
		}
	}

// Commander has to be run after the configuration.
}.Run();