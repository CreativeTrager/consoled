using System;

namespace Rumrunner0.Commander.Demo.Runnable;

/// <summary>
/// Dummy server with example methods to simulate basic server operations for demo purposes.
/// </summary>
internal sealed class DummyServer
{
	/// <summary>
	/// Prefix to be used in messages and logs.
	/// </summary>
	private const string _prefix = "[Dummy Server]";

	/// <summary>
	/// Initializes the dummy server. Prints message to the console.
	/// </summary>
	internal void Initialize() => Console.WriteLine($"{DummyServer._prefix} Initialized.");

	/// <summary>
	/// Starts the dummy server. Prints message to the console.
	/// </summary>
	internal void Start() => Console.WriteLine($"{DummyServer._prefix} Started.");

	/// <summary>
	/// Stops the dummy server. Prints message to the console.
	/// </summary>
	internal void Stop() => Console.WriteLine($"{DummyServer._prefix} Stopped.");

	/// <summary>
	/// Restarts the dummy server. Prints message to the console.
	/// </summary>
	internal void Restart() => Console.WriteLine($"{DummyServer._prefix} Restarted.");
}