using System;
using Rumble.Commander;

using var common = new CommonCommander(new ()
{
	Reader = Console.In,
	Writer = Console.Out
})
{
	CommandInputPrompt = "Please, enter a command",
	ConfirmationPrompt = "Should the command be executed?",

	UseAliases = true,
	MatchCase = true,
	AskForConfirmation = true,

	SystemCommandsOverrides = new ()
	{
		[SystemCommandNameWithAliases.Exit] = new ()
		{
			Aliases = new () { "eo", "qo" },
			ConfirmationPrompt = "Are you sure you want to exit?",

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

				UseAliases = true,
				MatchCase = true,
				AskForConfirmation = true
			}
		}
	}
}.Run();

// using var over = new ObjectCommander<X>(commandable: new X())
// {
// 	AskForConfirmation = true,
// 	CommandInputPrompt = "Object Enter a command",
// 	ConfirmationPrompt = "Object Are you sure?",
// 	Commands = new ()
// 	{
// 		new ()
// 		{
// 			Action = commandable => { },
// 			Settings = new ()
// 			{
// 				Name = "over1",
// 				Aliases = new () { "o1" },
// 				AskForConfirmation = true
// 			}
// 		},
// 		new ()
// 		{
// 			Action = commandable => { },
// 			Settings = new ()
// 			{
// 				Name = "over2",
// 				Aliases = new () { "o2" },
// 				AskForConfirmation = true
// 			}
// 		}
// 	}
// };

// // using var totalCommander = TotalCommander.Factory.Over(new [] { common, over }, options: new () {}).Run;
// using var totalCommander = TotalCommander.Factory.Over(common, over, options: new ()
// {
// 	AskForConfirmation = true,
// 	CommandInputPrompt = "Total Введите команду",
// 	ConfirmationPrompt = "Total Вы уверены?"
// }).Run;

// internal class X : IDisposable
// {
// 	public void Dispose()
// 	{
//
// 	}
// }