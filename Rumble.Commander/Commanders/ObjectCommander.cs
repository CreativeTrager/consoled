using System;
using System.Collections.Generic;
using System.Linq;
using Rumble.Commander.Commands;

namespace Rumble.Commander.Commanders;

///
/// <inheritdoc />
///
/// <typeparam name="TCommandable">Type of a commandable object over which an <see cref="ObjectCommander{TCommandable}"/> operates</typeparam>
public sealed class ObjectCommander<TCommandable> : Commander
{
	/// <summary>
	/// Commandable object over which an <see cref="ObjectCommander{TCommandable}"/> operates.
	/// </summary>
	private readonly TCommandable _commandable;

	/// <summary>
	/// Custom (user-defined) commands.
	/// </summary>
	private readonly List<Command<TCommandable>> _customCommands;

	/// <summary>
	///  Dictionary representation of the <see cref="_customCommands"/> linked to their names.
	/// </summary>
	private readonly Dictionary<string, Command<TCommandable>> _customCommandsDictionary;



	///
	/// <inheritdoc cref="_customCommands"/>
	///
	public required List<Command<TCommandable>> CustomCommands
	{
		init
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if(value is null)
			{
				return;
			}

			this._customCommands = value;
			this._customCommandsDictionary = this._customCommands.ToDictionary
			(
				keySelector: command => command.Settings.Name,
				elementSelector: command => command
			);
		}
	}

	///
	/// <inheritdoc />
	///
	protected override IReadOnlyCollection<ICommandSettingsContainer> CustomCommandsSettings
	{
		get => this._customCommands.Cast<ICommandSettingsContainer>().ToList();
	}



	///
	/// <inheritdoc cref="ObjectCommander{TCommandable}" />
	///
	public ObjectCommander(TCommandable commandable)
	{
		this._customCommands = new ();
		this._customCommandsDictionary = new ();
		this._commandable = commandable;
	}



	///
	/// <inheritdoc />
	///
	protected override void ExecuteCustomCommandByName(string name)
	{
		this._customCommandsDictionary[name].Action.Invoke(this._commandable);
	}

	///
	/// <inheritdoc />
	///
	public override void Dispose()
	{
		if(this._commandable is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}
}