using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rumble.Commander.Questions;

///
/// <inheritdoc />
///
internal record class QuestionResult : IQuestionResult
{
	///
	/// <inheritdoc cref="QuestionResult"/>
	///
	public QuestionResult()
	{
		this.Answer = string.Empty;
	}

	///
	/// <inheritdoc />
	///
	public string Answer { get; init; }

	///
	/// <inheritdoc />
	///
	public bool IsCorrect { get; init; }
}

///
/// <inheritdoc cref="IQuestionResult{TCheckTable}"/>
///
internal sealed record class QuestionResult<TCheckTable> : QuestionResult, IQuestionResult<TCheckTable>
where TCheckTable : ICheckTable, new()
{
	///
	/// <inheritdoc cref="QuestionResult{TCheckTable}"/>
	///
	public QuestionResult()
	{
		// Empty.
	}

	///
	/// <inheritdoc />
	///
	public bool Is(Expression<Func<TCheckTable, IEnumerable<string>>> checkGroupProvider)
	{
		return checkGroupProvider.Compile().Invoke(new ()).Contains(Answer);
	}
}