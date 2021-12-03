// ReSharper disable MissingIndent


namespace Bettermage.ConsoleUtils.ControlsHandler.Questions.PredefinedAnswersQuestions.Interfaces 
{
	internal interface IPredefinedAnswersQuestionBehaviour  
	{
		IPredefinedAnswersQuestionResult Ask();
	}
	internal interface IMultipleAnswersQuestionDataContainer 
	{
		string Question { get; init; }
		string[][] Answers { get; init; }
	}

	internal interface IPredefinedAnswersQuestion 
	: IPredefinedAnswersQuestionBehaviour,
	  IMultipleAnswersQuestionDataContainer 
	{
		// empty
	}
}
