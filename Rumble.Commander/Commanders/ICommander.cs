using System;

namespace Rumble.Commander.Commanders;

/// <summary>
/// Object that controls the application in the form of commands. <br />
/// It listens for input in the form of commands, which then execute a corresponding predefined action. <br />
/// It is <see cref="IDisposable"/>, allowing resources to be released when no longer in use.
/// </summary>
public interface ICommander : IDisposable
{
	/// <summary>
	/// Initiates the command listener loop. <br />
	/// Starts an interactive loop, where it listens for the input. <br />
	/// Once the command is entered, it executes the corresponding action. <br />
	/// This allows for dynamic and responsive control flow based on the input.
	/// </summary>
	/// <returns>The current instance of <see cref="ICommander"/>, allowing for chaining</returns>
	ICommander Run();
}