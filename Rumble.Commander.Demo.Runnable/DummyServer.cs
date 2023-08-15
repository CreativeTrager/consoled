using System;

namespace Rumble.Commander.Demo.Runnable;

/// <summary>
/// Dummy server with example methods to simulate basic server operations for demo purposes.
/// </summary>
internal sealed class DummyServer
{
	/// <summary>
	/// Initializes the dummy server.
	/// Prints message to the console.
	/// </summary>
	internal void Initialize() => Console.WriteLine("[Dummy Server] Initialized.");

	/// <summary>
	/// Starts the dummy server.
	/// Prints message to the console.
	/// </summary>
	internal void Start() => Console.WriteLine("[Dummy Server] Started.");

	/// <summary>
	/// Stops the dummy server.
	/// Prints message to the console.
	/// </summary>
	internal void Stop() => Console.WriteLine("[Dummy Server] Stopped.");

	/// <summary>
	/// Restarts the dummy server.
	/// Prints message to the console.
	/// </summary>
	internal void Restart() => Console.WriteLine("[Dummy Server] Restarted.");
}