using System.Collections.Generic;

namespace Rumble.Commander;

/// <summary>
/// Settings of the <see cref="ICommand"/>.
/// </summary>
public sealed class CommandSettings
{
	/// <summary>
	/// Name of the command.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Aliases of the command.
	/// </summary>
	public List<string> Aliases { get; set; }

	/// <summary>
	/// Flag that indicates whether to ask for confirmation before the command execution.
	/// </summary>
	/// <remarks>
	/// Controlled by <see cref="ICommander"/> by default.
	/// Set the value to override the default behavior for this particular command.
	/// </remarks>
	public bool? AskForConfirmation { get; set; }

	/// <summary>
	/// Confirmation prompt.
	/// </summary>
	/// <remarks>
	/// Controlled by <see cref="ICommander"/> by default.
	/// Set the value to override the default value for this particular command.
	/// </remarks>
	public string? ConfirmationPrompt { get; set; }

	/// <summary>
	/// Flag that indicates whether the aliases of the command should be used.
	/// </summary>
	/// <remarks>
	/// Controlled by <see cref="ICommander"/> by default.
	/// Set the value to override the default behavior for this particular command.
	/// </remarks>
	public bool? UseAliases { get; set; }

	/// <summary>
	/// Flag that indicates whether the case of the command name or alias should match.
	/// </summary>
	/// <remarks>
	/// Controlled by <see cref="ICommander"/> by default.
	/// Set the value to override the default behavior for this particular command.
	/// </remarks>
	public bool? MatchCase { get; set; }

	/// <summary>
	/// <see cref="Name"/> with <see cref="Aliases"/> represented in one collection.
	/// </summary>
	public IReadOnlyCollection<string> NameWithAliases => new List<string>() { Name, Aliases }.AsReadOnly();

	///
	/// <inheritdoc cref="Command" />
	///
	public CommandSettings()
	{
		this.Aliases = new List<string>();
	}
}