using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rumble.Commander.Questions;

internal class Question : IQuestion
{
	private readonly string _prompt;
	private readonly string[] _correctAnswers;

	public string Prompt
	{
		get => _prompt;
		init => _prompt = value;
	}

	public string[] CorrectAnswers
	{
		get => _correctAnswers;
		init => _correctAnswers = value;
	}

	internal Question(in string prompt, in IEnumerable<string> correctAnswers)
	{
		this._prompt = prompt;
		this._correctAnswers = correctAnswers.ToArray();
	}

	public IQuestionResult AskObsessively()
	{
		while(true)
		{
			Console.Write($"{this._prompt}: ");
			var input = Console.ReadLine() ?? string.Empty;

			if(this._correctAnswers.FirstOrDefault(answer => answer.Equals(input)) is not null)
			{
				return new QuestionResult() { Answer = input };
			}
		}
	}
}

internal class QuestionResult : IQuestionResult
{
	public string Answer { get; init; }
}

internal sealed class QuestionResult<TCheckTable> : QuestionResult, IQuestionResult<TCheckTable>
where TCheckTable : ICheckTable, new()
{
	public bool Is(Expression<Func<TCheckTable, IReadOnlyCollection<string>>> check)
	{
		return check.Compile().Invoke(new ()).Contains(Answer);
	}
}