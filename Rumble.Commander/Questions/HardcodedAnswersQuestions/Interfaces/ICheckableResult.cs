using System;
using System.Linq.Expressions;


namespace Rumble.Commander.Questions.HardcodedAnswersQuestions.Interfaces;
internal interface ICheckableResult<TCheckTable> where TCheckTable : ICheckTable 
{
	bool Is(Expression<Func<TCheckTable, string[]>> check);
}