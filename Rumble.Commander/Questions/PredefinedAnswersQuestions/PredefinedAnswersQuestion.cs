using System;
using System.Linq;

namespace Rumble.Commander.Questions.PredefinedAnswersQuestions;

internal sealed class PredefinedAnswersQuestion : IPredefinedAnswersQuestion
{
	private readonly string _question;
	private readonly string[][] _answers;

	public string Question
	{
		get => _question;
		init => _question = value;
	}
	public string[][] Answers
	{
		get => _answers;
		init => _answers = value;
	}

	internal PredefinedAnswersQuestion(string question, params string[][] answers)
	{
		this._question = question;
		this._answers = answers;
	}

	public IPredefinedAnswersQuestionResult Ask()
	{
		while(true)
		while(true)
		{
			var answerPrimaries = string.Join
			(
				separator: "/",
				values: this._answers.Select(aliases => aliases[0])
			);

			Console.Write($"{this._question} ({answerPrimaries}): ");
			var input = Console.ReadLine()!.ToLower();
			var answerWithAliases = this._answers.FirstOrDefault(answer => answer.Contains(input));

			if(answerWithAliases is not null)
			{
				return new PredefinedAnswersQuestionResult()
				{
					Answer = answerWithAliases
				};
			}
		}
	}
}