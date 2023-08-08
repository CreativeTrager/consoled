using System;
using Rumble.Commander.Commanders;

namespace Rumble.Commander.Commands;

/// <summary>
/// Entity that is used by <see cref="ICommander"/>.
/// </summary>
public interface ICommand
{
	/// <summary>
	/// Action that is executed when the command invoked.
	/// </summary>
	Action Action { get; init; }

	/// <summary>
	/// Settings of the command.
	/// </summary>
	CommandSettings Settings { get; init; }
}

///
/// <inheritdoc cref="ICommand"/>
///
/// <typeparam name="TCommandable">Type of a commandable object the command operates on</typeparam>
public interface ICommand<TCommandable>
{
	///
	/// <inheritdoc cref="ICommand.Action"/>
	///
	/// <typeparam name="TCommandable">Type of a commandable object the command operates on</typeparam>
	Action<TCommandable> Action { get; init; }

	///
	/// <inheritdoc cref="ICommand.Settings"/>
	///
	CommandSettings Settings { get; init;}
}