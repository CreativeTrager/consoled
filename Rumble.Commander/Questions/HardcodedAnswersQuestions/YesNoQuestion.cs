using System.Collections.Generic;
using Rumble.Commander.Questions.PredefinedAnswersQuestions;

namespace Rumble.Commander.Questions.HardcodedAnswersQuestions;

internal sealed class YesNoQuestion : IHardcodedAnswersQuestion<YesNoQuestion.YesNoAnswersTable>
{
	internal sealed class YesNoAnswersTable : ICheckTable
	{
		internal (string Name, string[] Aliases) Yes { get; } = ("yes", new [] { "y" });
		internal (string Name, string[] Aliases) No  { get; } = ("no" , new [] { "n" });

		internal IReadOnlyList<string> YesFlat => new List<string>(collection: Yes.Aliases) { Yes.Name }.AsReadOnly();
		internal IReadOnlyList<string> NoFlat => new List<string>(collection: No.Aliases) { No.Name }.AsReadOnly();
	}

	private readonly PredefinedAnswersQuestion _rPredefinedAnswersQuestion;
	internal YesNoQuestion(string question)
	{
		var answerTable = new YesNoAnswersTable();
		this._rPredefinedAnswersQuestion = new
		(
			question,
			answers: new List<IEnumerable<string>>()
			{
				answerTable.YesFlat,
				answerTable.NoFlat
			}
		);
	}

	public IHardcodedAnswersQuestionResult<YesNoAnswersTable> Ask()
	{
		return new HardcodedAnswersQuestionResult<YesNoAnswersTable>()
		{
			Answer = _rPredefinedAnswersQuestion.Ask().Answer
		};
	}

	IPredefinedAnswersQuestionResult IPredefinedAnswersQuestionBehaviour.Ask()
	{
		return Ask();
	}
}