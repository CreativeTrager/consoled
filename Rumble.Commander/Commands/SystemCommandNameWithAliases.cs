using System.Collections.Generic;
using System.Linq;
using Rumble.Commander.Commanders;

namespace Rumble.Commander.Commands;

/// <summary>
/// Name and aliases of a system command used by <see cref="ICommander"/>.
/// </summary>
public sealed class SystemCommandNameWithAliases
{
	/// <summary>
	/// Name of the system command.
	/// </summary>
	private readonly string _name;

	/// <summary>
	/// Aliases of the system command.
	/// </summary>
	private readonly List<string> _aliases;

	///
	/// <inheritdoc cref="SystemCommandNameWithAliases"/>
	///
	/// <param name="name">Name of the system command</param>
	/// <param name="aliases">Aliases of the system command</param>
	private SystemCommandNameWithAliases(string name, List<string> aliases)
	{
		this._name = name;
		this._aliases = aliases;
	}

	///
	/// <inheritdoc cref="_name"/>
	///
	public string Name => this._name;

	///
	/// <inheritdoc cref="_aliases"/>
	///
	public List<string> Aliases => this._aliases.ToList();

	///
	/// <inheritdoc cref="object.ToString"/>
	///
	/// <returns><see cref="string"/> representation of the system command</returns>
	public override string ToString()
	{
		return $"Name: \"{this._name}\"";
	}

	/// <summary>
	/// Exit system command used to break out from the <see cref="ICommander"/>'s run loop.
	/// </summary>
	public static SystemCommandNameWithAliases Exit => new (name: "exit", aliases: new () { "quit", "q" });

	/// <summary>
	/// Help system command used to show information about available commands for particular <see cref="ICommander"/>.
	/// </summary>
	public static SystemCommandNameWithAliases Help => new (name: "help", aliases: new () { "about", "h" });
}