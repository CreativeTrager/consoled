using System;
using System.Linq.Expressions;


// ReSharper disable MissingIndent


namespace Bettermage.ConsoleUtils.ControlsHandler.Questions.HardcodedAnswersQuestions.Interfaces 
{
	internal interface ICheckableResult<TCheckTable>
	where TCheckTable : ICheckTable 
	{
		bool Is(Expression<Func<TCheckTable, string[]>> check);
	}
}
