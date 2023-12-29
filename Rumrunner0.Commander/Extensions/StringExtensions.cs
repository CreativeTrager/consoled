using System.Collections.Generic;

namespace Rumrunner0.Commander.Extensions;

/// <summary>
/// Extensions for type <see cref="string" />.
/// </summary>
internal static class StringExtensions
{
	///
	/// <inheritdoc cref="string.Join(string,IEnumerable{string?})" />
	///
	internal static string Join(this IEnumerable<string> source, string separator = " ")
	{
		return string.Join(separator, source);
	}
}