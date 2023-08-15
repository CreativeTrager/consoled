namespace Rumble.Commander.Questions;

/// <summary>
/// Result of a question.
/// </summary>
internal interface IQuestionResult
{
	/// <summary>
	/// Provided answer for the question.
	/// </summary>
	string Answer { get; init; }

	/// <summary>
	/// Flag that indicates whether the answer of the question result is correct.
	/// </summary>
	bool IsCorrect { get; init; }

	/// <summary>
	/// Flag that indicates whether the answer is incorrect. Equivalent to <c>IsCorrect is false</c>.
	/// </summary>
	bool IsIncorrect => IsCorrect is false;
}

/// <summary>
/// Result of a question which answer can be checked against a specific check table.
/// </summary>
/// <typeparam name="TCheckTable">Type of the check table used to check the answer of the question result.</typeparam>
internal interface IQuestionResult<TCheckTable> : IQuestionResult, ICheckableResult<TCheckTable>
where TCheckTable : ICheckTable
{
	// Empty.
}