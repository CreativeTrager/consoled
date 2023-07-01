using System;
using System.Collections.Generic;

namespace Rumble.Commander;

public sealed class CommanderSettings
{
	public bool AskForConfirmation { get; init; }
	public required Dictionary<string[], Action> Commands { get; init; }
}

public sealed class CommanderSettings<TCommandable>
where TCommandable : class, IDisposable
{
	public bool AskForConfirmation { get; init; }
	public required Dictionary<string[], Action<TCommandable>> Commands { get; init; }
}

public sealed class CommanOptions
{
	public bool AskForConfirmation { get; init; }
}

public sealed class CommandOptions<TCommandable>
where TCommandable : class
{
	public bool AskForConfirmation { get; init; }
}