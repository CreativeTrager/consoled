using System;
using System.Linq;
using System.Text;
using Bettermage.ConsoleUtils.ControlsHandler.Questions.PredefinedAnswersQuestions.Interfaces;


namespace Bettermage.ConsoleUtils.ControlsHandler.Questions.PredefinedAnswersQuestions;
internal sealed class PredefinedAnswersQuestion : IPredefinedAnswersQuestion 
{
	private readonly string rQuestion;
	private readonly string[][] rAnswers;

	public string Question 
	{
		get => rQuestion;
		init => rQuestion = value;
	}
	public string[][] Answers 
	{
		get => rAnswers;
		init => rAnswers = value;
	}

	internal PredefinedAnswersQuestion(
		string question, params string[][] answers
	) 
	{
		this.rQuestion = question;
		this.rAnswers = answers;
	}

	public IPredefinedAnswersQuestionResult Ask() 
	{
		while(true)
		while(true) 
		{
			var answerPrimaries = string.Join(
				separator: "/",
				values: rAnswers.Select(aliases => aliases[0])
			);

			Console.Write(
				new StringBuilder()
					.Append($"{rQuestion} ")
					.Append($"({answerPrimaries}): ")
					.ToString()
			);

			var input = (
				Console.ReadLine() ??
				string.Empty
			).Trim().ToLower();

			var answerWithAliases = rAnswers.SingleOrDefault(
				answerWithAliases => answerWithAliases.Contains(input)
			);

			if(answerWithAliases is not null) 
			{
				return new PredefinedAnswersQuestionResult() {
					Answer = answerWithAliases
				};
			}
		}
	}
}