namespace Rumrunner0.Commander.Questions;

/// <summary>
/// Question with a prompt and a set of correct answers.
/// </summary>
internal interface IQuestion
{
	/// <summary>
	/// Prompt for the question.
	/// </summary>
	string Prompt { get; init; }

	/// <summary>
	/// Correct answers for the question.
	/// </summary>
	string[] CorrectAnswers { get; init; }

	/// <summary>
	/// Asks the question and returns the result.
	/// </summary>
	/// <returns>Result of the question.</returns>
	IQuestionResult Ask();
}

/// <summary>
/// Question that is checkable against a check table.
/// </summary>
/// <typeparam name="TCheckTable">Type of the check table used to check the answer of the question result.</typeparam>
internal interface IQuestion<TCheckTable> : IQuestion where TCheckTable : ICheckTable
{
	/// <summary>
	/// Asks the question and returns the result, which can be checked against specified check table.
	/// </summary>
	/// <returns>Result of the question, including information required for checking against specified check table.</returns>
	new IQuestionResult<TCheckTable> Ask();
}