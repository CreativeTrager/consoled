using System.Collections.Generic;

namespace Rumble.Commander.Commands;

/// <summary>
/// Settings overrides for <see cref="ICommand"/>.
/// </summary>
public sealed record class CommandOverride
{
	///
	/// <inheritdoc cref="CommandOverride"/>
	///
	public CommandOverride()
	{
		this.Aliases = new ();
	}

	///
	/// <inheritdoc cref="CommandSettings.Aliases"/>
	///
	public List<string> Aliases { get; init; }

	///
	/// <inheritdoc cref="CommandSettings.ConfirmationPrompt"/>
	///
	public string? ConfirmationPrompt { get; init; }

	///
	/// <inheritdoc cref="CommandSettings.UseAliases"/>
	///
	public bool? UseAliases { get; init; }

	///
	/// <inheritdoc cref="CommandSettings.MatchCase"/>
	///
	public bool? MatchCase { get; init; }

	///
	/// <inheritdoc cref="CommandSettings.AskForConfirmation"/>
	///
	public bool? AskForConfirmation { get; init; }
}