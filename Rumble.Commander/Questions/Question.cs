using System;
using System.Collections.Generic;
using System.Linq;

namespace Rumble.Commander.Questions;

///
/// <inheritdoc />
///
internal class Question : IQuestion
{
	///
	/// <inheritdoc cref="Prompt"/>
	///
	private readonly string _prompt;

	///
	/// <inheritdoc cref="CorrectAnswers"/>
	///
	private readonly string[] _correctAnswers;

	///
	/// <inheritdoc cref="Prompt"/>
	///
	private readonly QuestionSettings _settings;

	///
	/// <inheritdoc cref="Question"/>
	///
	internal Question
	(
		in string prompt,
		in IEnumerable<string> correctAnswers,
		in QuestionSettings settings
	)
	{
		this._prompt = prompt;
		this._correctAnswers = correctAnswers.ToArray();
		this._settings = settings;
	}

	///
	/// <inheritdoc />
	///
	public string Prompt
	{
		get => this._prompt;
		init
		{
			if(string.IsNullOrWhiteSpace(value)) { return; }
			this._prompt = value;
		}
	}

	///
	/// <inheritdoc />
	///
	public string[] CorrectAnswers
	{
		get => this._correctAnswers;
		init
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if(value is null)
			{
				return;
			}

			this._correctAnswers = value;
		}
	}

	///
	/// <inheritdoc />
	///
	public IQuestionResult Ask()
	{
		this._settings.Writer.Write($"{this._prompt} {this._settings.InputPrefix}");
		var input = this._settings.Reader.ReadLine() ?? string.Empty;

		return new QuestionResult()
		{
			Answer = input,
			IsCorrect = this._correctAnswers.Contains
			(
				input,
				comparer: this._settings.MatchCase
					? StringComparer.CurrentCulture
					: StringComparer.CurrentCultureIgnoreCase
			)
		};
	}
}

///
/// <inheritdoc cref="IQuestion{TCheckTable}"/>
///
internal sealed class Question<TCheckTable> : Question, IQuestion<TCheckTable> where TCheckTable : ICheckTable, new()
{
	///
	/// <inheritdoc cref="Question{TCheckTable}"/>
	///
	internal Question
	(
		in string prompt,
		in QuestionSettings settings

	) : base
	(
		prompt,
		correctAnswers: new YesNoAnswersTable().All,
		settings
	)
	{
		// Empty.
	}

	///
	/// <inheritdoc />
	///
	public new IQuestionResult<TCheckTable> Ask()
	{
		var baseQuestionResult = base.Ask();
		return new QuestionResult<TCheckTable>()
		{
			Answer = baseQuestionResult.Answer,
			IsCorrect = baseQuestionResult.IsCorrect
		};
	}

	///
	/// <inheritdoc />
	///
	IQuestionResult IQuestion.Ask()
	{
		return Ask();
	}
}