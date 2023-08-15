using System.Collections.Generic;
using Rumble.Commander.Extensions;

namespace Rumble.Commander.Questions;

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
	public IEnumerable<string> Yes
	{
		get => new List<string>() { "yes", "y" }.AsReadOnly();
	}

	/// <summary>
	/// "No" check group.
	/// </summary>
	public IEnumerable<string> No
	{
		get => new List<string>() { "no", "n" }.AsReadOnly();
	}

	///
	/// <inheritdoc />
	///
	public IEnumerable<string> All
	{
		get => new List<string>() { this.Yes, this.No }.AsReadOnly();
	}
}