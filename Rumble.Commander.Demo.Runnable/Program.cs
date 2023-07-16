using System;
using Rumble.Commander;

using var common = new CommonCommander()
{
	MatchCase = false,
	AskForConfirmation = true,
	CommandInputPrompt = "Common Enter a command",
	ConfirmationPrompt = "Common Are you sure?",
	SystemCommandsOverrides = new ()
	{
		[SystemCommandNameWithAliases.Exit] = new ()
		{
			Name = "exitLALA",
			Aliases = new () { "exLA" },
			MatchCase = true,
			UseAliases = true,
			AskForConfirmation = true,
			ConfirmationPrompt = "Are you sure you want to exit?"
		}
	},
	CustomCommands = new ()
	{
		new ()
		{
			Action = () => Console.WriteLine($"common1 (c1) executed"),
			Settings = new ()
			{
				Name = "common1",
				Aliases = new () { "c1" },
				AskForConfirmation = true,
				ConfirmationPrompt = "Are you sure to execute common1?"
			}
		},
		new ()
		{
			Action = () => Console.WriteLine($"common2 (c2) executed"),
			Settings = new ()
			{
				Name = "common2",
				Aliases = new () { "c2" },
				AskForConfirmation = true
			}
		}
	}
}.RunSelf();

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

internal class X : IDisposable
{
	public void Dispose()
	{

	}
}