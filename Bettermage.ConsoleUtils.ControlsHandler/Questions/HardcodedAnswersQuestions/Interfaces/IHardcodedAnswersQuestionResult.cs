using Bettermage.ConsoleUtils.ControlsHandler.Questions.PredefinedAnswersQuestions.Interfaces;


// ReSharper disable MissingIndent


namespace Bettermage.ConsoleUtils.ControlsHandler.Questions.HardcodedAnswersQuestions.Interfaces 
{
	internal interface IHardcodedAnswersQuestionResult
	<TCheckTable> 
	
	: IPredefinedAnswersQuestionResult,
	  ICheckableResult<TCheckTable>
	
	where TCheckTable
	: ICheckTable 
	
	
	{
		// empty
	}
}
