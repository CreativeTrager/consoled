using System.Collections.Generic;

namespace Rumble.Commander.Questions;

/// <summary>
/// Table containing check groups used to verify
/// if a answer of the question belongs to a specific check group.
/// </summary>
internal interface ICheckTable
{
	/// <summary>
	/// All entries from all check groups in a flat form.
	/// </summary>
	IEnumerable<string> All { get; }
}