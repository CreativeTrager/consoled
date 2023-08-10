using System;
using Rumble.Commander.Commanders;
using Rumble.Commander.Commands;

// using var commonCommander = new ObjectCommander<TextReader>(File.OpenText("test.txt"))

using var commonCommander = new CommonCommander()
{
	Settings = new ()
	{
		Reader = Console.In,
		Writer = Console.Out,

		InputPrompt = "Please, enter a command",
		ConfirmationPrompt = "Should the command be executed?",

		UseAliases = true,
		MatchCase = true,
		AskForConfirmation = true
	},

	SystemCommandsOverrides = new ()
	{
		[SystemCommandNames.Quit] = new ()
		{
			Aliases = new () { "eo", "qo" },
			ConfirmationPrompt = "Are you sure you want to quit?",

			UseAliases = true,
			MatchCase = true,
			AskForConfirmation = true
		}
	},

	CustomCommands = new ()
	{
		new ()
		{
			Action = () => Console.WriteLine($"common1 executed"),
			Settings = new ()
			{
				Name = "common1",
				Aliases = new () { "c1" },
				ConfirmationPrompt = "Are you sure to execute common1?",

				UseAliases = true,
				MatchCase = true,
				AskForConfirmation = true
			}
		},
		new ()
		{
			Action = () => Console.WriteLine($"common2 has been executed"),
			Settings = new ()
			{
				Name = "common2",
				Aliases = new () { "c2" },
				ConfirmationPrompt = "Are you sure to execute common2?",

				UseAliases = false,
				MatchCase = false,
				AskForConfirmation = true
			}
		}
	}
}.Run();