namespace Rumble.Commander.Questions;

internal interface IQuestionResult
{
	bool IsCorrect { get; init; }
	string Answer { get; init; }

	bool IsIncorrect => IsCorrect is false;
}

internal interface IQuestionResult<TCheckTable> : IQuestionResult, ICheckableResult<TCheckTable>
where TCheckTable : ICheckTable
{
	// Empty.
}