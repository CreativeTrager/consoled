using Bettermage.ConsoleUtils.ControlsHandler.Questions.PredefinedAnswersQuestions.Interfaces;


// ReSharper disable MissingIndent


namespace Bettermage.ConsoleUtils.ControlsHandler.Questions.PredefinedAnswersQuestions 
{
	internal sealed class PredefinedAnswersQuestionResult
	: IPredefinedAnswersQuestionResult 
	{
		public string[] Answer { get; init; }
	}
}
