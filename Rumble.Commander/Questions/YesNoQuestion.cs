using System.Collections.Generic;

namespace Rumble.Commander.Questions;

internal sealed class YesNoQuestion : Question, IQuestion<YesNoQuestion.YesNoAnswersTable>
{
	internal sealed class YesNoAnswersTable : ICheckTable
	{
		internal (string Name, string[] Aliases) Yes { get; } = ("yes", new [] { "y" });
		internal (string Name, string[] Aliases) No  { get; } = ("no" , new [] { "n" });

		internal IEnumerable<string> YesFlat => new List<string>() { Yes.Name, Yes.Aliases }.AsReadOnly();
		internal IEnumerable<string> NoFlat  => new List<string>() { No.Name, No.Aliases }.AsReadOnly();
		internal IEnumerable<string> AllFlat => new List<string>() { YesFlat, NoFlat };
	}

	internal YesNoQuestion(string prompt, QuestionSettings settings) : base(prompt, correctAnswers: new YesNoAnswersTable().AllFlat, settings)
	{
		// Empty.
	}

	public new IQuestionResult<YesNoAnswersTable> Ask()
	{
		var baseQuestionResult = base.Ask();
		return new QuestionResult<YesNoAnswersTable>()
		{
			Answer = baseQuestionResult.Answer,
			IsCorrect = baseQuestionResult.IsCorrect
		};
	}

	IQuestionResult IQuestion.Ask()
	{
		return Ask();
	}
}