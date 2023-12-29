using System.Collections.Generic;

namespace Rumrunner0.Commander.Questions;

///
/// <inheritdoc />
///
internal sealed class YesNoAnswersTable : ICheckTable
{
	///
	/// <inheritdoc cref="YesNoAnswersTable"/>
	///
	public YesNoAnswersTable()
	{
		// Empty.
	}

	/// <summary>
	/// "Yes" check group.
	/// </summary>
	public IEnumerable<string> Yes => ((List<string>)[ "yes", "y" ]).AsReadOnly();

	/// <summary>
	/// "No" check group.
	/// </summary>
	public IEnumerable<string> No => ((List<string>)[ "no", "n" ]).AsReadOnly();

	///
	/// <inheritdoc />
	///
	public IEnumerable<string> All => ((List<string>)[ ..Yes, ..No ]).AsReadOnly();
}