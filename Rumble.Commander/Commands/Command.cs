using System;

namespace Rumble.Commander.Commands;

///
/// <inheritdoc />
///
public sealed class Command : ICommand
{
	///
	/// <inheritdoc />
	///
	public required Action Action { get; init; }

	///
	/// <inheritdoc />
	///
	public required CommandSettings Settings { get; init; }

	///
	/// <inheritdoc cref="Command" />
	///
	public Command()
	{
		// Empty.
	}
}

///
/// <inheritdoc />
///
public sealed class Command<TCommandable> : ICommand<TCommandable>
{
	///
	/// <inheritdoc />
	///
	public required Action<TCommandable> Action { get; init; }

	///
	/// <inheritdoc />
	///
	public required CommandSettings Settings { get; init; }

	///
	/// <inheritdoc cref="Command{TCommandable}" />
	///
	public Command()
	{
		// Empty.
	}
}