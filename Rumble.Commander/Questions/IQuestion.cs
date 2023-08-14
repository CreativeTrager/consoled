namespace Rumble.Commander.Questions;

internal interface IQuestion
{
	string Prompt { get; init; }
	string[] CorrectAnswers { get; init; }
	IQuestionResult Ask();
}

internal interface IQuestion<TResultCheckTable> : IQuestion where TResultCheckTable : ICheckTable
{
	new IQuestionResult<TResultCheckTable> Ask();
}