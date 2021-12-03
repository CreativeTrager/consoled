using System;
using System.Linq;
using System.Linq.Expressions;
using Bettermage.ConsoleUtils.ControlsHandler.Questions.HardcodedAnswersQuestions.Interfaces;


// ReSharper disable MissingIndent


namespace Bettermage.ConsoleUtils.ControlsHandler.Questions.HardcodedAnswersQuestions 
{
	internal sealed class HardcodedAnswersQuestionResult
	<TCheckTable>
	
	: IHardcodedAnswersQuestionResult
	  <TCheckTable>  
	
	where TCheckTable
	: ICheckTable, new()
	
	
	{
		public string[] Answer { get; init; }
		public bool Is(Expression<Func<TCheckTable, string[]>> check) 
		{
			return check.Compile().Invoke(new ())
			   .SequenceEqual(Answer);
		}
	}
}
