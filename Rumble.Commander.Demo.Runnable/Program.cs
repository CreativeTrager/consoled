using System;
using Rumble.Commander;

using var common = new CommonCommander(settings: new ()
{
	AskForConfirmation = true,
	CommandInputPrompt = "Common Введите команду",
	ConfirmationPrompt = "Common Вы уверены?",
	Commands =
	{
		new Command()
		{
			Name = "common1",
			Aliases = new [] { "c1" },
			AskForConfirmation = false,
			Action = () =>
			{

			}
		},
		new Command()
		{
			Name = "common2",
			Aliases = new [] { "c2" },
			AskForConfirmation = true,
			Action = () =>
			{

			}
		}
	}
});

using var over = new ObjectCommander<X>(commandable: new X(), settings: new ()
{
	AskForConfirmation = true,
	CommandInputPrompt = "Object Введите команду",
	ConfirmationPrompt = "Object Вы уверены?",
	Commands = new ()
	{
		new Command()
		{
			Name = "over1",
			Aliases = new [] { "01" },
			AskForConfirmation = false,
			Action = () =>
			{

			}
		},
		new Command()
		{
			Name = "over2",
			Aliases = new [] { "o2" },
			AskForConfirmation = true,
			Action = () =>
			{

			}
		}
	}
});

// using var totalCommander = TotalCommander.Factory.Over(new [] { common, over }, options: new () {}).Run;
using var totalCommander = TotalCommander.Factory.Over(common, over, options: new ()
{
	AskForConfirmation = true,
	CommandInputPrompt = "Total Введите команду",
	ConfirmationPrompt = "Total Вы уверены?"
}).Run;

internal class X : IDisposable
{

	public void Dispose()
	{

	}
}