using System;
using System.Collections.Generic;
using System.Linq;

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
		this._settings.Writer.Write($"{this._prompt} {this._settings.InputPrefix}");
		var input = this._settings.Reader.ReadLine() ?? string.Empty;

		return new QuestionResult()
		{
			Answer = input,
			IsCorrect = this._correctAnswers.Contains
			(
				input, this._settings.MatchCase
					? StringComparer.CurrentCulture
					: StringComparer.CurrentCultureIgnoreCase
			)
		};
	}
}