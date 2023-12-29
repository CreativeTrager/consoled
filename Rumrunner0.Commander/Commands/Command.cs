using System;

namespace Rumrunner0.Commander.Commands;

///
/// <inheritdoc />
///
public sealed partial class Command : ICommand
{
	///
	/// <inheritdoc cref="Command" />
	///
	public Command()
	{
		// Empty.
	}

	///
	/// <inheritdoc />
	///
	public required Action Action { get; init; }

	///
	/// <inheritdoc />
	///
	public required CommandSettings Settings { get; init; }
}

///
/// <inheritdoc />
///
public sealed class Command<TCommandable> : ICommand<TCommandable>
{
	///
	/// <inheritdoc cref="Command{TCommandable}" />
	///
	public Command()
	{
		// Empty.
	}

	///
	/// <inheritdoc />
	///
	public required Action<TCommandable> Action { get; init; }

	///
	/// <inheritdoc />
	///
	public required CommandSettings Settings { get; init; }
}