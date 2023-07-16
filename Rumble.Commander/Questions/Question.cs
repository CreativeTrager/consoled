using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Rumble.Essentials;

namespace Rumble.Commander.Questions;

internal class Question : IQuestion
{
	private readonly string _prompt;
	private readonly string[] _correctAnswers;
	private readonly QuestionSettings _settings;

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

	internal Question(in string prompt, in IEnumerable<string> correctAnswers, in QuestionSettings settings)
	{
		this._prompt = prompt;
		this._correctAnswers = correctAnswers.ToArray();
		this._settings = settings;
	}

	public IQuestionResult Ask()
	{
		while(true)
		{
			this._settings.Writer.Write($"{this._prompt} {this._settings.InputPrefix}");
			var input = this._settings.Reader.ReadLine() ?? string.Empty;

			if(this._correctAnswers.FirstOrDefault(answer => answer.Equals(input)) is not null)
			{
				return new QuestionResult() { Answer = input };
			}

			if(string.IsNullOrWhiteSpace(input) is false && this._settings.ShowHint)
			{
				this._settings.Writer.WriteLine($"{this._settings.Hint}");
			}
		}
	}
}

internal class QuestionSettings
{
	internal required TextReader Reader { get; init; }
	internal required TextWriter Writer { get; init; }

	public string InputPrefix { get; set; }

	internal bool ShowHint { get; init; }
	internal string Hint { get; init; }

	internal QuestionSettings()
	{
		this.InputPrefix = ">";

		this.ShowHint = false;
		this.Hint = string.Empty;
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