using System;

namespace Rumble.Commander;

/// <summary>
/// Commander that provides a structure for creating interactive command line interfaces (CLIs).
/// It listens for input in the form of commands, which then execute a corresponding action.
/// It is <see cref="IDisposable"/>, allowing resources to be released when no longer in use.
/// </summary>
public interface ICommander : IDisposable
{
	/// <summary>
	/// Initiates the command listener loop.
	/// Starts an interactive loop, where it listens for the input.
	/// Once the command is entered, it executes the corresponding action.
	/// This allows for dynamic and responsive control flow based on the input.
	/// </summary>
	/// <returns>The current instance of <see cref="ICommander"/>, allowing for chaining</returns>
	ICommander RunSelf();
}