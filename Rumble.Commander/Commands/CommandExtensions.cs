using System;
using System.Collections.Generic;

namespace Rumble.Commander.Commands;

/// <summary>
/// Extensions for <see cref="ICommand"/>.
/// </summary>
public static class CommandExtensions
{
	/// <summary>
	/// Name with or without aliases of the <see cref="ICommand"/>
	/// depending on the <see cref="CommandSettings"/>.<see cref="CommandSettings.UseAliases"/> result
	/// or, as fallback,"<paramref name="useAliases"/>" execution result.
	/// </summary>
	/// <param name="source">The command which name and aliases are targeted.</param>
	/// <param name="useAliases">
	/// Additional check whether to use aliases or not that involves external objects and states.
	/// The check is used ONLY IF setting is not specified in the <see cref="CommandSettings"/>.
	/// </param>
	/// <returns>Name with or without aliases of the <see cref="ICommand"/>.</returns>
	/// <remarks>The extension makes it possible to add fallback validation for using aliases or not in addition to the internal one.</remarks>
	public static IReadOnlyCollection<string> AvailableNamesUsingFallback(this ICommandSettingsContainer source, Func<bool>? useAliases)
	{
		return new List<string>()
		{
			source.Settings.Name,
			source.Settings.UseAliases ?? useAliases?.Invoke() ?? false
				? source.Settings.Aliases : ArraySegment<string>.Empty
		};
	}
}