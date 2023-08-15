using System;
using Rumble.Commander.Commanders;

namespace Rumble.Commander.Commands;

/// <summary>
/// Abstraction that is used by <see cref="ICommander"/> to execute specific action.
/// </summary>
public interface ICommand : ICommandSettingsContainer
{
	/// <summary>
	/// Action that is executed when the command is invoked.
	/// </summary>
	Action Action { get; init; }
}

///
/// <inheritdoc cref="ICommand"/>
///
/// <typeparam name="TCommandable">Type of a commandable object the command operates on.</typeparam>
public interface ICommand<TCommandable> : ICommandSettingsContainer
{
	///
	/// <inheritdoc cref="ICommand.Action"/>
	///
	Action<TCommandable> Action { get; init; }
}