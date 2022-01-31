using Bettermage.ConsoleUtils.ControlsHandler.Questions.PredefinedAnswersQuestions.Interfaces;


namespace Bettermage.ConsoleUtils.ControlsHandler.Questions.HardcodedAnswersQuestions.Interfaces;
internal interface IHardcodedAnswersQuestionResult<TCheckTable> : IPredefinedAnswersQuestionResult, ICheckableResult<TCheckTable>
where TCheckTable : ICheckTable 
{
	// empty
}