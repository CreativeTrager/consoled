using System;
using System.Collections.Generic;

namespace Rumble.Commander;

///
/// <inheritdoc />
///
/// <typeparam name="TCommandable">Type of a commandable object over which an <see cref="ObjectCommander{TCommandable}"/> operates</typeparam>
public sealed class ObjectCommander<TCommandable> : ICommander
where TCommandable : class, IDisposable
{
	public bool? AskForConfirmation { get; init; }
	public string? CommandInputPrompt { get; set; }
	public string? ConfirmationPrompt { get; set; }
	public required List<Command<TCommandable>> Commands { get; init; }
	public Dictionary<SystemCommandNameWithAliases, CommandOverride> SystemCommandsOverrides { get; set; }

	private readonly TCommandable _commandable;

	///
	/// <inheritdoc cref="ObjectCommander{TCommandable}" />
	///
	public ObjectCommander(TCommandable commandable)
	{
		this._commandable = commandable;
	}

	///
	/// <inheritdoc />
	///
	public ICommander Run()
	{
		// while(true)
		// {
		// 	var answer = new PredefinedAnswersQuestion
		// 	(
		// 		question: $"Enter command",
		// 		answers: this._settings.Commands.Keys.ToArray()
		// 	).Ask().Answer;
		//
		// 	var confirm = true;
		// 	if(this._settings.AskForConfirmation is true)
		// 	{
		// 		confirm = new YesNoQuestion(question: $"Are you sure?").Ask().Is(answers => answers.Yes);
		// 	}
		//
		// 	if(confirm)
		// 	{
		// 		_settings.Commands[answer].Invoke(_commandable);
		// 	}
		// }
		//
		return this;
	}

	///
	/// <inheritdoc />
	///
	public void Dispose()
	{
		this._commandable.Dispose();
	}
}