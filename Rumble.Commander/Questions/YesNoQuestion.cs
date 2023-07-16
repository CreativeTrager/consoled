using System.Collections.Generic;

namespace Rumble.Commander.Questions;

internal sealed class YesNoQuestion : Question, IQuestion<YesNoQuestion.YesNoAnswersTable>
{
	internal sealed class YesNoAnswersTable : ICheckTable
	{
		internal (string Name, string[] Aliases) Yes { get; } = ("yes", new [] { "y" });
		internal (string Name, string[] Aliases) No  { get; } = ("no" , new [] { "n" });

		internal IReadOnlyCollection<string> YesFlat => new List<string>(collection: Yes.Aliases) { Yes.Name }.AsReadOnly();
		internal IReadOnlyCollection<string> NoFlat => new List<string>(collection: No.Aliases) { No.Name }.AsReadOnly();
		internal IReadOnlyCollection<string> AllFlat => new List<string>() { YesFlat, NoFlat };
	}

	internal YesNoQuestion(string prompt) : base(prompt, correctAnswers: new YesNoAnswersTable().AllFlat)
	{
		// Empty
	}

	public IQuestionResult<YesNoAnswersTable> AskObsessively()
	{
		return new QuestionResult<YesNoAnswersTable>() { Answer = base.AskObsessively().Answer };
	}

	IQuestionResult IQuestion.AskObsessively()
	{
		return AskObsessively();
	}
}