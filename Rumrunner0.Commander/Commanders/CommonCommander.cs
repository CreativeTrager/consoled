using System.Collections.Generic;
using System.Linq;
using Rumrunner0.Commander.Commands;

namespace Rumrunner0.Commander.Commanders;

///
/// <inheritdoc />
///
public sealed class CommonCommander : Commander
{
	/// <summary>
	/// Custom (user-defined) commands.
	/// </summary>
	private readonly List<Command> _customCommands;

	/// <summary>
	///  Dictionary representation of the <see cref="_customCommands"/> linked to their names.
	/// </summary>
	private readonly Dictionary<string, Command> _customCommandsDictionary;

	///
	/// <inheritdoc cref="CommonCommander" />
	///
	public CommonCommander() : base()
	{
		this._customCommands = [];
		this._customCommandsDictionary = [];
	}



	///
	/// <inheritdoc cref="_customCommands"/>
	///
	public required List<Command> CustomCommands
	{
		init
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (value is null)
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
	/// <inheritdoc />
	///
	protected override void ExecuteCustomCommandByName(string name)
	{
		this._customCommandsDictionary[name].Action.Invoke();
	}

	///
	/// <inheritdoc />
	///
	public override void Dispose()
	{
		// Empty.
	}
}