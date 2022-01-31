using Bettermage.ConsoleUtils.ControlsHandler.Questions.PredefinedAnswersQuestions.Interfaces;


namespace Bettermage.ConsoleUtils.ControlsHandler.Questions.PredefinedAnswersQuestions;
internal sealed class PredefinedAnswersQuestionResult : IPredefinedAnswersQuestionResult 
{
	public string[] Answer { get; init; }
}