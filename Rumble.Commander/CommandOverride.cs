using System.Collections.Generic;

namespace Rumble.Commander;

/// <summary>
/// Settings overrides for <see cref="ICommand"/>.
/// </summary>
public sealed class CommandOverride
{
	///
	/// <inheritdoc cref="CommandSettings.Name"/>
	///
	public string? Name { get; init; }

	///
	/// <inheritdoc cref="CommandSettings.Aliases"/>
	///
	public List<string> Aliases { get; init; }

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

	///
	/// <inheritdoc cref="CommandOverride"/>
	///
	public CommandOverride()
	{
		this.Aliases = new ();
	}
}