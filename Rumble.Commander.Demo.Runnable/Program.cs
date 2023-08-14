using System;
using System.IO;
using Rumble.Commander.Commanders;
using Rumble.Commander.Commands;

using var commonCommander = new ObjectCommander<StreamReader>(File.OpenText("test.txt"))
{
	Settings = new ()
	{
		Writer = Console.Out,
		Reader = Console.In,

		InputPrompt = "Please, enter a command",
		ConfirmationPrompt = "Should the command be executed?",

		UseAliases = true,
		MatchCase = true,
		AskForConfirmation = true
	},

	SystemCommandsOverrides = new ()
	{
		[Command.System.Name.Quit] = new ()
		{
			Aliases = new () { "qq" },
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
			Action = reader =>
			{
				Console.WriteLine(reader.ReadLine());
			},
			Settings = new ()
			{
				Name = "read-line",
				Aliases = new () { "rl" },
				ConfirmationPrompt = "Are you sure to execute common1?",

				UseAliases = true,
				MatchCase = true,
				AskForConfirmation = true
			}
		},
		new ()
		{
			Action = reader =>
			{
				Console.WriteLine(reader.ReadToEnd());
			},
			Settings = new ()
			{
				Name = "read-to-end",
				Aliases = new () { "rte" },
				UseAliases = false,
				MatchCase = false,
				AskForConfirmation = true
			}
		},
		new ()
		{
			Action = reader =>
			{
				reader.BaseStream.Seek(0, SeekOrigin.Begin);
				reader.DiscardBufferedData();
			},
			Settings = new ()
			{
				Name = "reset",
				UseAliases = false,
				MatchCase = false,
				AskForConfirmation = true
			}
		}
	}
}.Run();