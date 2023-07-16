using System.Collections.Generic;

namespace Rumble.Commander.Questions;

internal sealed class YesNoQuestion : Question, IQuestion<YesNoQuestion.YesNoAnswersTable>
{
	internal sealed class YesNoAnswersTable : ICheckTable
	{
		internal (string Name, string[] Aliases) Yes { get; } = ("yes", new [] { "y" });
		internal (string Name, string[] Aliases) No  { get; } = ("no" , new [] { "n" });

		internal IReadOnlyCollection<string> YesFlat => new List<string>() { Yes.Name, Yes.Aliases }.AsReadOnly();
		internal IReadOnlyCollection<string> NoFlat  => new List<string>() { No.Name, No.Aliases }.AsReadOnly();
		internal IReadOnlyCollection<string> AllFlat => new List<string>() { YesFlat, NoFlat };
	}

	internal YesNoQuestion(string prompt, QuestionSettings settings) : base(prompt, correctAnswers: new YesNoAnswersTable().AllFlat, settings)
	{
		// Empty
	}

	public IQuestionResult<YesNoAnswersTable> Ask()
	{
		return new QuestionResult<YesNoAnswersTable>() { Answer = base.Ask().Answer };
	}

	IQuestionResult IQuestion.Ask()
	{
		return Ask();
	}
}