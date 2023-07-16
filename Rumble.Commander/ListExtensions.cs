using System.Collections.Generic;

namespace Rumble.Commander;

internal static class ListExtensions
{
	internal static List<T> Add<T>(this List<T> source, IEnumerable<T> range)
	{
		source.AddRange(range);
		return source;
	}
}