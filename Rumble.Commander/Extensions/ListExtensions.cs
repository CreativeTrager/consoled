using System;
using System.Collections.Generic;

namespace Rumble.Commander.Extensions;

/// <summary>
/// Extensions for <see cref="List{T}"/>.
/// </summary>
internal static class ListExtensions
{
	/// <summary>
	/// Adds the elements of the "<paramref name="range"/>" collection to the end of the "<paramref name="source"/>" collection.
	/// </summary>
	/// <param name="source"><see cref="List{T}"/> to which the elements from the "<paramref name="range"/>" collection will be added.</param>
	/// <param name="range">Collection which will be added to the "<paramref name="source"/>" collection.</param>
	/// <typeparam name="T">Type of the list elements.</typeparam>
	/// <returns>Current instance of the <see cref="List{T}"/>, allowing for chaining.</returns>
	/// <exception cref="ArgumentNullException">Thrown, if <paramref name="range"/> is null.</exception>
	internal static List<T> Add<T>(this List<T> source, IEnumerable<T> range)
	{
		ArgumentNullException.ThrowIfNull(range);
		source.AddRange(range);
		return source;
	}
}