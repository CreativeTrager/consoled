namespace Rumble.Commander.Questions.PredefinedAnswersQuestions;

internal interface IPredefinedAnswersQuestion : IPredefinedAnswersQuestionBehaviour, IMultipleAnswersQuestionDataContainer
{
	// empty
}

internal interface IPredefinedAnswersQuestionBehaviour
{
	IPredefinedAnswersQuestionResult Ask();
}

internal interface IMultipleAnswersQuestionDataContainer
{
	string Question { get; init; }
	string[][] Answers { get; init; }
}

public interface IPredefinedAnswersQuestionResult
{
	string[] Answer { get; init; }
}