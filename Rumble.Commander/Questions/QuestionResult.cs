using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rumble.Commander.Questions;

internal class QuestionResult : IQuestionResult
{
	public QuestionResult()
	{
		this.Answer = string.Empty;
	}

	public string Answer { get; init; }
	public bool IsCorrect { get; init; }
}

internal sealed class QuestionResult<TCheckTable> : QuestionResult, IQuestionResult<TCheckTable>
where TCheckTable : ICheckTable, new()
{
	public QuestionResult()
	{
		// Empty.
	}

	public bool Is(Expression<Func<TCheckTable, IEnumerable<string>>> check)
	{
		return check.Compile().Invoke(new ()).Contains(Answer);
	}
}