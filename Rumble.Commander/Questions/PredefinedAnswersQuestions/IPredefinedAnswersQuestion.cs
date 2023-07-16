namespace Rumble.Commander.Questions.PredefinedAnswersQuestions;

internal interface IPredefinedAnswersQuestion : IPredefinedAnswersQuestionBehaviour, IMultipleAnswersQuestionData
{
	// empty
}

internal interface IPredefinedAnswersQuestionBehaviour
{
	IPredefinedAnswersQuestionResult Ask();
}

internal interface IMultipleAnswersQuestionData
{
	string Question { get; init; }
	string[][] Answers { get; init; }
}