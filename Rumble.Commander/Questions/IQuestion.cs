using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Rumble.Commander.Questions;

internal interface IQuestion
{
	string Prompt { get; init; }
	string[] CorrectAnswers { get; init; }
	IQuestionResult Ask();
}

internal interface IQuestionResult
{
	string Answer { get; init; }
}



internal interface IQuestion<TResultCheckTable> : IQuestion
where TResultCheckTable : ICheckTable
{
	new IQuestionResult<TResultCheckTable> Ask();
}

internal interface IQuestionResult<TCheckTable> : IQuestionResult, ICheckableResult<TCheckTable>
where TCheckTable : ICheckTable
{
	// Empty
}



internal interface ICheckableResult<TCheckTable>
where TCheckTable : ICheckTable
{
	bool Is(Expression<Func<TCheckTable, IReadOnlyCollection<string>>> check);
}

internal interface ICheckTable
{
	// Empty
}