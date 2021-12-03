using Bettermage.ConsoleUtils.ControlsHandler.Questions.PredefinedAnswersQuestions.Interfaces;


// ReSharper disable MissingIndent


namespace Bettermage.ConsoleUtils.ControlsHandler.Questions.HardcodedAnswersQuestions.Interfaces 
{
	internal interface IHardcodedAnswersQuestion 
	<TResultCheckTable> 
	
	: IPredefinedAnswersQuestionBehaviour
	
	where TResultCheckTable
	: ICheckTable
	
	
	{
		new IHardcodedAnswersQuestionResult<TResultCheckTable> Ask();
	}
}
